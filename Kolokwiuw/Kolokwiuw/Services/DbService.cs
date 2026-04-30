using Kolokwiuw.DTOs;
using Kolokwiuw.Exceptions;
using Kolokwiuw.Services;
using Microsoft.Data.SqlClient;

namespace Kolokwium.Services;

public class DbService : IDbService
{
    private readonly string _connectionString;

    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                            ?? throw new InvalidOperationException(
                                "Brak ConnectionString 'DefaultConnection' w konfiguracji!");
    }

    public async Task<GetVendorDetailsDTO> GetVendorAsync(string code)
    {
        var checkVendroExistance = "SELECT 1 FROM Vendors WHERE Code = @code";
        var query =
            "SELECT v.code AS VendorCode,\n       v.name AS VendorName, \n       p.id AS ProductId,\n       p.name AS ProductName, \n       p.description AS ProductDescription,\n       p.StickerPrice AS ProductStickerPrice,\n       m.Id AS MakerId, \n       m.Name AS MakerName, \n       pt.id AS ProductTypeID, \n       pt.name AS ProductTypeName, \n       vp.amount AS Amount, \n       vp.pricePerUnit AS PricePerUnit \nFROM Vendors v\nJOIN VendorProducts vp ON v.Code = vp.VendorCode\nJOIN Products p ON p.Id = vp.ProductId\nJOIN ProductTypes pt ON pt.Id = p.ProductTypeId\nJOIN Makers m ON m.Id = p.MakerId\nWHERE v.Code = @code\n;";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var checkVendorCommand = new SqlCommand();
        checkVendorCommand.Connection = connection;
        checkVendorCommand.CommandText = checkVendroExistance;
        checkVendorCommand.Parameters.AddWithValue("@code", code);

        if (await checkVendorCommand.ExecuteScalarAsync() is null)
            throw new NotFoundException("Vendor of code: " + code + " not found!");

        await using var command = new SqlCommand();
        command.Connection = connection;

        command.Parameters.Clear();
        command.CommandText = query;
        command.Parameters.AddWithValue("@code", code);

        GetVendorDetailsDTO result = null;

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            if (result is null)
                result = new GetVendorDetailsDTO
                    { code = code, name = reader.GetString(reader.GetOrdinal("VendorName")) };

            result.products.Add(new GetProductsDetailsDTO
            {
                Id = reader.GetInt32(reader.GetOrdinal("ProductId")),
                name = reader.GetString(reader.GetOrdinal("ProductName")),
                description = reader.GetString(reader.GetOrdinal("ProductDescription")),
                strickerPrice = reader.GetDecimal(reader.GetOrdinal("ProductStickerPrice")),
                productType = new GetProductTypeDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("MakerId")),
                    name = reader.GetString(reader.GetOrdinal("MakerName"))
                },
                maker = new GetMakerDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ProductTypeID")),
                    name = reader.GetString(reader.GetOrdinal("ProductTypeName"))
                },
                vendorOffer = new GetVendorOfferDTO
                {
                    amout = reader.GetInt32(reader.GetOrdinal("Amount")),
                    pricePerUnit = reader.GetDecimal(reader.GetOrdinal("PricePerUnit"))
                }
            });
        }

        return result ?? throw new NotFoundException("No records for this Vendor found!");
    }

    public async Task AddAsync(CreateVendorDTO dto)
    {
        var addVendorSql = "INSERT INTO Vendors VALUES (@Code, @Name)";
        var addProductsSql = "INSERT INTO VendorProducts VALUES (@ProductId, @code, @amout, @pricePerUnit);\n";
        
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        await using var transaction = connection.BeginTransaction();
        
        await using var command = new SqlCommand();
        command.Connection = connection;
        command.Transaction = transaction;
        command.CommandText = addVendorSql;
        try
        {
            command.Parameters.AddWithValue("@code", dto.code);
            command.Parameters.AddWithValue("@name", dto.name);
            await command.ExecuteNonQueryAsync();

            foreach (var product in dto.products)
            {
                try
                {
                    var InsertProductsCommand = new SqlCommand(addProductsSql, connection);
                    InsertProductsCommand.Transaction = transaction;
                    InsertProductsCommand.Parameters.AddWithValue("@ProductId", product.id);
                    InsertProductsCommand.Parameters.AddWithValue("@code", dto.code);
                    InsertProductsCommand.Parameters.AddWithValue("@amout", product.amount);
                    InsertProductsCommand.Parameters.AddWithValue("@pricePerUnit", product.pricePerUnit);
                    await InsertProductsCommand.ExecuteNonQueryAsync();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new NotFoundException("Product not found");
                }
            }
            transaction.Commit();
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new ConflictException("Vendro already exists!");
        }
    }
}