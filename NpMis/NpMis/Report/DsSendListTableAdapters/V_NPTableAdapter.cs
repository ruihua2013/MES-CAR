namespace NpMis.Report.DsSendListTableAdapters
{
    using NpMis.Properties;
    using NpMis.Report;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Diagnostics;

    [HelpKeyword("vs.data.TableAdapter"), DataObject(true), Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), DesignerCategory("code"), ToolboxItem(true)]
    public class V_NPTableAdapter : Component
    {
        private SqlDataAdapter _adapter;
        private bool _clearBeforeFill;
        private SqlCommand[] _commandCollection;
        private SqlConnection _connection;

        [DebuggerNonUserCode]
        public V_NPTableAdapter()
        {
            this.ClearBeforeFill = true;
        }

        [HelpKeyword("vs.data.TableAdapter"), DebuggerNonUserCode, DataObjectMethod(DataObjectMethodType.Fill, true)]
        public virtual int Fill(DsSendList.V_NPDataTable dataTable)
        {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if (this.ClearBeforeFill)
            {
                dataTable.Clear();
            }
            return this.Adapter.Fill(dataTable);
        }

        [DebuggerNonUserCode, DataObjectMethod(DataObjectMethodType.Select, true), HelpKeyword("vs.data.TableAdapter")]
        public virtual DsSendList.V_NPDataTable GetData()
        {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            DsSendList.V_NPDataTable dataTable = new DsSendList.V_NPDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }

        [DebuggerNonUserCode]
        private void InitAdapter()
        {
            this._adapter = new SqlDataAdapter();
            DataTableMapping mapping = new DataTableMapping {
                SourceTable = "Table",
                DataSetTable = "V_NP"
            };
            mapping.ColumnMappings.Add("NpNo", "NpNo");
            mapping.ColumnMappings.Add("Code", "Code");
            mapping.ColumnMappings.Add("CodeName", "CodeName");
            mapping.ColumnMappings.Add("PlanID", "PlanID");
            mapping.ColumnMappings.Add("PlanID1", "PlanID1");
            mapping.ColumnMappings.Add("TaskID", "TaskID");
            mapping.ColumnMappings.Add("SendID", "SendID");
            mapping.ColumnMappings.Add("BarCode", "BarCode");
            mapping.ColumnMappings.Add("PlanTime", "PlanTime");
            mapping.ColumnMappings.Add("DeadLine", "DeadLine");
            this._adapter.TableMappings.Add(mapping);
        }

        [DebuggerNonUserCode]
        private void InitCommandCollection()
        {
            this._commandCollection = new SqlCommand[] { new SqlCommand() };
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT NpNo, Code, CodeName, PlanID, PlanID1, TaskID, SendID, BarCode, PlanTime, DeadLine FROM dbo.V_NP";
            this._commandCollection[0].CommandType = CommandType.Text;
        }

        [DebuggerNonUserCode]
        private void InitConnection()
        {
            this._connection = new SqlConnection();
            this._connection.ConnectionString = Settings.Default.TC_VPMISConnectionString;
        }

        [DebuggerNonUserCode]
        private SqlDataAdapter Adapter
        {
            get
            {
                if (this._adapter == null)
                {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }

        [DebuggerNonUserCode]
        public bool ClearBeforeFill
        {
            get
            {
                return this._clearBeforeFill;
            }
            set
            {
                this._clearBeforeFill = value;
            }
        }

        [DebuggerNonUserCode]
        protected SqlCommand[] CommandCollection
        {
            get
            {
                if (this._commandCollection == null)
                {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }

        [DebuggerNonUserCode]
        internal SqlConnection Connection
        {
            get
            {
                if (this._connection == null)
                {
                    this.InitConnection();
                }
                return this._connection;
            }
            set
            {
                this._connection = value;
                if (this.Adapter.InsertCommand != null)
                {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if (this.Adapter.DeleteCommand != null)
                {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if (this.Adapter.UpdateCommand != null)
                {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; i < this.CommandCollection.Length; i++)
                {
                    if (this.CommandCollection[i] != null)
                    {
                        this.CommandCollection[i].Connection = value;
                    }
                }
            }
        }
    }
}

