using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Vault> Get(string currentUser)
    {
      string sql = "SELECT * FROM Vaults WHERE currentUser = @UserId";
      return _db.Query<Vault>(sql);
    }

    internal Vault Create(Vault newVault)
    {
      string sql = @"
            INSERT INTO vaults
            (name, description, userId)
            VALUES
            (@Name, @Description, @UserId);
            SELECT LAST_INSERT_ID()";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      return newVault;
    }

    internal Vault Get(int id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @Id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { Id = id });
    }

    internal IEnumerable<Vault> GetUserVaults(string UserId)
    {
      string sql = "SELECT * FROM vaults WHERE userId = @UserId";
      return _db.Query<Vault>(sql, new { UserId });
    }

    internal Vault Edit(Vault updatedVault)
    {
      string sql = @"
        UPDATE vaults
        SET
        WHERE id = @id
        ";
      _db.Execute(sql, updatedVault);
      return updatedVault;
    }

    internal bool Delete(int Id)
    {
      string sql = "DELETE FROM vaults WHERE id = @Id LIMIT 1";
      int removed = _db.Execute(sql, new { Id });
      return removed == 1;
    }

    internal IEnumerable<VaultKeepViewModel> GetByVaultId(int vaultId, string userId)
    {
      string sql = @"
                SELECT 
                    k.*,
                    vk.id as vaultKeepId
                FROM vaultkeeps vk
                INNER JOIN keeps k ON k.id = vk.keepId 
                WHERE (vaultId = @vaultId AND vk.userId = @userId)";
      return _db.Query<VaultKeepViewModel>(sql, new { vaultId, userId });
    }
  }
}