using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RaftLabs.Enterprise.Database.Databases
{
    public static class DbContextCaseSensitive
    {
        /// <summary>
        /// Set table's name to Uppercase
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ToUpperCaseTables(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.GetTableName().ToUpper());
            }
        }

        /// <summary>
        /// Set column's name to Uppercase 
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ToUpperCaseColumns(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName(new StoreObjectIdentifier()).ToUpper());
                }
            }
        }

        /// <summary>
        /// Set foreignkey's name to Uppercase
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ToUpperCaseForeignKeys(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    foreach (IMutableForeignKey fk in entityType.FindForeignKeys(property))
                    {
                        fk.SetConstraintName(fk.GetConstraintName().ToUpper());
                    }
                }
            }
        }

        /// <summary>
        /// Set index's name to Uppercase
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ToUpperCaseIndexes(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (IMutableIndex index in entityType.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName().ToUpper());
                }
            }
        }
    }
}
