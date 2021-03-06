using System;
using System.Data;
using System.Data.Common;

namespace NHibernate.Dialect.Schema
{
	public class MsSqlDataBaseSchema: AbstractDataBaseSchema
	{
		public MsSqlDataBaseSchema(DbConnection connection) : base(connection) {}

		public override ITableMetadata GetTableMetadata(DataRow rs, bool extras)
		{
			return new MsSqlTableMetadata(rs, this, extras);
		}
	}

	public class MsSqlTableMetadata: AbstractTableMetadata
	{
		public MsSqlTableMetadata(DataRow rs, IDataBaseSchema meta, bool extras) : base(rs, meta, extras) { }

		protected override void ParseTableInfo(DataRow rs)
		{
			Catalog = Convert.ToString(rs["TABLE_CATALOG"]);
			Schema = Convert.ToString(rs["TABLE_SCHEMA"]);
			if (string.IsNullOrEmpty(Catalog)) Catalog = null;
			if (string.IsNullOrEmpty(Schema)) Schema = null;
			Name = Convert.ToString(rs["TABLE_NAME"]);
		}

		protected override string GetConstraintName(DataRow rs)
		{
			return Convert.ToString(rs["CONSTRAINT_NAME"]);
		}

		protected override string GetColumnName(DataRow rs)
		{
			return Convert.ToString(rs["COLUMN_NAME"]);
		}

		protected override string GetIndexName(DataRow rs)
		{
			return Convert.ToString(rs["INDEX_NAME"]);
		}

		protected override IColumnMetadata GetColumnMetadata(DataRow rs)
		{
			return new MsSqlColumnMetadata(rs);
		}

		protected override IForeignKeyMetadata GetForeignKeyMetadata(DataRow rs)
		{
			return new MsSqlForeignKeyMetadata(rs);
		}

		protected override IIndexMetadata GetIndexMetadata(DataRow rs)
		{
			return new MsSqlIndexMetadata(rs);
		}
	}

	public class MsSqlColumnMetadata : AbstractColumnMetaData
	{
		public MsSqlColumnMetadata(DataRow rs) : base(rs)
		{
			Name = Convert.ToString(rs["COLUMN_NAME"]);
			object aValue;

			aValue = rs["CHARACTER_MAXIMUM_LENGTH"];
			if (aValue != DBNull.Value)
				ColumnSize = Convert.ToInt32(aValue);

			aValue = rs["NUMERIC_PRECISION"];
			if (aValue != DBNull.Value)
				NumericalPrecision = Convert.ToInt32(aValue);

			Nullable = Convert.ToString(rs["IS_NULLABLE"]);
			TypeName = Convert.ToString(rs["DATA_TYPE"]);			
		}
	}

	public class MsSqlIndexMetadata: AbstractIndexMetadata
	{
		public MsSqlIndexMetadata(DataRow rs) : base(rs)
		{
			Name = Convert.ToString(rs["INDEX_NAME"]);
		}
	}

	public class MsSqlForeignKeyMetadata : AbstractForeignKeyMetadata
	{
		public MsSqlForeignKeyMetadata(DataRow rs)
			: base(rs)
		{
			Name = Convert.ToString(rs["CONSTRAINT_NAME"]);
		}
	}
}
