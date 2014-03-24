using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace DesignPatterns
{
    //the class calling our adapter
    public class DataRenderer
    {
        private readonly IDbDataAdapter _dataAdapter;
        private StubAdapter adapter;

        public DataRenderer(IDbDataAdapter dataAdapter)
        {
            _dataAdapter = dataAdapter;
        }

        public void Render(TextWriter writer)
        {
            writer.WriteLine("Rendering Data:");
            var myDataSet = new DataSet();

            _dataAdapter.Fill(myDataSet);

            foreach (DataTable table in myDataSet.Tables)
            {
                foreach (DataColumn column in table.Columns)
                {
                    writer.Write(column.ColumnName.PadRight(20) + " ");
                }
                writer.WriteLine();
                foreach (DataRow row in table.Rows)
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        writer.Write(row[i].ToString().PadRight(20) + " ");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
    //our concrete adapters, we could have a SQL adapter, OleDB adapter...etc
    //IDbDataAdapter is a .Net adapter interface for dealing with databases
    //this stub adapter is just for testing
    public class StubAdapter : IDbDataAdapter
    {
        //this is the only method we care about
        public int Fill(DataSet dataSet)
        {
            var myDataTable = new DataTable();
            myDataTable.Columns.Add(new DataColumn("Id", typeof(int)));
            myDataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            myDataTable.Columns.Add(new DataColumn("Description", typeof(string)));

            var myRow = myDataTable.NewRow();
            myRow[0] = 1;
            myRow[1] = "Adapter";
            myRow[2] = "Adapter description";
            myDataTable.Rows.Add(myRow);
            dataSet.Tables.Add(myDataTable);
            dataSet.AcceptChanges();

            return 1;
        }

        public IDbCommand DeleteCommand
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbCommand InsertCommand
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbCommand SelectCommand
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbCommand UpdateCommand
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
        {
            throw new NotImplementedException();
        }

        public IDataParameter[] GetFillParameters()
        {
            throw new NotImplementedException();
        }

        public MissingMappingAction MissingMappingAction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public MissingSchemaAction MissingSchemaAction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ITableMappingCollection TableMappings
        {
            get { throw new NotImplementedException(); }
        }

        public int Update(DataSet dataSet)
        {
            throw new NotImplementedException();
        }
    }
}
