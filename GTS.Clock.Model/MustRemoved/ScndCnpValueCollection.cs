using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTS.Clock.Model.Concepts;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using GTS.Clock.Infrastructure;

namespace GTS.Clock.Model.MustRemoved
{
    public class ScndCnpValueCollection<TKey, TValue> : Dictionary<TKey, TValue>
        where TValue : BaseScndCnpValue
    {

        #region Cunstructor

        public ScndCnpValueCollection()
        {
            this.InsertRows = new DataTable();
            this.InsertRows.Columns.Add("ScndCnpValue_SecondaryConceptId", typeof(decimal));
            this.InsertRows.Columns.Add("ScndCnpValue_Index");
            this.InsertRows.Columns.Add("ScndCnpValue_PersonId", typeof(decimal));
            this.InsertRows.Columns.Add("ScndCnpValue_Value", typeof(decimal));
            this.InsertRows.Columns.Add("ScndCnpValue_FromPairs");
            this.InsertRows.Columns.Add("ScndCnpValue_ToPairs");
            this.InsertRows.Columns.Add("ScndCnpValue_IsValid", typeof(bool));
            this.InsertRows.Columns.Add("ScndCnpValue_FromDate");
            this.InsertRows.Columns.Add("ScndCnpValue_ToDate");
            this.InsertRows.Columns.Add("ScndCnpValue_Type");
            this.InsertRows.Columns.Add("ScndCnpValue_CalcRangeGrpId");
            this.InsertRows.Columns.Add("ScndCnpValue_CalcDateRangeId");
        }

        #endregion

        #region Variables

        private string updateQuery;

        SqlBulkCopy SqlBulkInsert;
        DataTable InsertRows;

        #endregion

        #region Properties


        public string UpdateQuery
        {
            get
            {
                if (this.updateQuery == null || this.updateQuery.Length == 0)
                {
                    foreach (TKey key in base.Keys)
                    {
                        TValue value = base[key];

                        if (value.ID != 0)
                        {
                            this.updateQuery += String.Format(";UPDATE TA_SecondaryConceptValue SET {0} = {2} WHERE {1} = {3} ",
                                                                  "ScndCnpValue_Value", "ScndCnpValue_ID",
                                                                     value.Value, value.ID);
                        }
                    }
                }
                return this.updateQuery;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// یک آیتم را براساس کلید مشخص شده به لیست اضافه می کند
        /// اگر با این کلید قبلا آیتمی وجود داشت بروزرسانی می شود
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            if (base.Keys.Contains(key))
            {
                base[key] = value;
            }
            else
            {
                base.Add(key, value);
            }
        }

        public void DoInsert(IDbConnection connection, IDbTransaction Transaction)
        {
            foreach (TKey key in base.Keys)
            {
                TValue value = base[key];

                if (value.ID == 0)
                {
                    if (value is IPairableConceptValue<IPair>)
                    {
                        IPairableConceptValue<IPair> PValue = (IPairableConceptValue<IPair>)value;
                        foreach (IPair pair in PValue.Pairs)
                        {
                            value.FromPairs += String.Format("{0};", pair.From.ToString());
                            value.ToPairs += String.Format("{0};", pair.To.ToString());
                        }
                    }

                    DataRow row = this.InsertRows.NewRow();
                    row["ScndCnpValue_SecondaryConceptId"] = value.Concept.ID;
                    row["ScndCnpValue_Index"] = value.Index;
                    row["ScndCnpValue_PersonId"] = value.Person.ID;
                    row["ScndCnpValue_Value"] = value.Value;
                    row["ScndCnpValue_FromPairs"] = value.FromPairs;
                    row["ScndCnpValue_ToPairs"] = value.ToPairs;
                    row["ScndCnpValue_IsValid"] = true;
                    row["ScndCnpValue_FromDate"] = value.FromDate.ToShortDateString();
                    row["ScndCnpValue_ToDate"] = value.ToDate.ToShortDateString();
                    row["ScndCnpValue_Type"] = value.Concept.Type;
                    row["ScndCnpValue_CalcRangeGrpId"] = value.CalcRangeGrpId;
                    row["ScndCnpValue_CalcDateRangeId"] = value.CalcDateRangeId;

                    switch (value.Concept.PersistSituationType)
                    {
                        case ScndCnpPersistSituationType.Persistable:
                            if (value is IPairableConceptValue<IPair>)
                            {
                                if (value.Value != 0 || ((IPairableConceptValue<IPair>)value).PairValues != 0)
                                {
                                    this.InsertRows.Rows.Add(row);
                                }
                            }
                            else
                                if (value.Value != 0)
                                {
                                    this.InsertRows.Rows.Add(row);
                                }
                            break;
                        case ScndCnpPersistSituationType.NotPersist: break;
                        case ScndCnpPersistSituationType.AlwaysPersist:
                            this.InsertRows.Rows.Add(row);
                            break;
                    }
                }
            }

            this.SqlBulkInsert = new SqlBulkCopy((SqlConnection)connection, SqlBulkCopyOptions.Default, (SqlTransaction)Transaction);
            SqlBulkInsert.DestinationTableName = "dbo.TA_SecondaryConceptValue";
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_SecondaryConceptId", "ScndCnpValue_SecondaryConceptId"));
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_Index", "ScndCnpValue_Index"));
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_PersonId", "ScndCnpValue_PersonId"));
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_Value", "ScndCnpValue_Value"));
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_FromPairs", "ScndCnpValue_FromPairs"));
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_ToPairs", "ScndCnpValue_ToPairs"));
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_IsValid", "ScndCnpValue_IsValid"));
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_FromDate", "ScndCnpValue_FromDate"));
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_ToDate", "ScndCnpValue_ToDate"));
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_Type", "ScndCnpValue_Type"));
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_CalcRangeGrpId", "ScndCnpValue_CalcRangeGrpId"));
            SqlBulkInsert.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ScndCnpValue_CalcDateRangeId", "ScndCnpValue_CalcDateRangeId"));
            SqlBulkInsert.WriteToServer(this.InsertRows);
        }


        #endregion
    }
}
