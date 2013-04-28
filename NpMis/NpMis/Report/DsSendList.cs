namespace NpMis.Report
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlRoot("DsSendList"), DesignerCategory("code"), ToolboxItem(true), XmlSchemaProvider("GetTypedDataSetSchema"), HelpKeyword("vs.data.DataSet")]
    public class DsSendList : DataSet
    {
        private System.Data.SchemaSerializationMode _schemaSerializationMode;
        private V_ListDataTable tableV_List;
        private V_NPDataTable tableV_NP;
        private V_SendListDataTable tableV_SendList;

        [DebuggerNonUserCode]
        public DsSendList()
        {
            this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            base.BeginInit();
            this.InitClass();
            CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += handler;
            base.Relations.CollectionChanged += handler;
            base.EndInit();
        }

        [DebuggerNonUserCode]
        protected DsSendList(SerializationInfo info, StreamingContext context) : base(info, context, false)
        {
            this._schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            if (base.IsBinarySerialized(info, context))
            {
                this.InitVars(false);
                CollectionChangeEventHandler handler = new CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += handler;
                this.Relations.CollectionChanged += handler;
            }
            else
            {
                string s = (string) info.GetValue("XmlSchema", typeof(string));
                if (base.DetermineSchemaSerializationMode(info, context) == System.Data.SchemaSerializationMode.IncludeSchema)
                {
                    DataSet dataSet = new DataSet();
                    dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                    if (dataSet.Tables["V_List"] != null)
                    {
                        base.Tables.Add(new V_ListDataTable(dataSet.Tables["V_List"]));
                    }
                    if (dataSet.Tables["V_NP"] != null)
                    {
                        base.Tables.Add(new V_NPDataTable(dataSet.Tables["V_NP"]));
                    }
                    if (dataSet.Tables["V_SendList"] != null)
                    {
                        base.Tables.Add(new V_SendListDataTable(dataSet.Tables["V_SendList"]));
                    }
                    base.DataSetName = dataSet.DataSetName;
                    base.Prefix = dataSet.Prefix;
                    base.Namespace = dataSet.Namespace;
                    base.Locale = dataSet.Locale;
                    base.CaseSensitive = dataSet.CaseSensitive;
                    base.EnforceConstraints = dataSet.EnforceConstraints;
                    base.Merge(dataSet, false, MissingSchemaAction.Add);
                    this.InitVars();
                }
                else
                {
                    base.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
                }
                base.GetSerializationData(info, context);
                CollectionChangeEventHandler handler2 = new CollectionChangeEventHandler(this.SchemaChanged);
                base.Tables.CollectionChanged += handler2;
                this.Relations.CollectionChanged += handler2;
            }
        }

        [DebuggerNonUserCode]
        public override DataSet Clone()
        {
            DsSendList list = (DsSendList) base.Clone();
            list.InitVars();
            list.SchemaSerializationMode = this.SchemaSerializationMode;
            return list;
        }

        [DebuggerNonUserCode]
        protected override XmlSchema GetSchemaSerializable()
        {
            MemoryStream w = new MemoryStream();
            base.WriteXmlSchema(new XmlTextWriter(w, null));
            w.Position = 0L;
            return XmlSchema.Read(new XmlTextReader(w), null);
        }

        [DebuggerNonUserCode]
        public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
        {
            DsSendList list = new DsSendList();
            XmlSchemaComplexType type = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            xs.Add(list.GetSchemaSerializable());
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = list.Namespace
            };
            sequence.Items.Add(item);
            type.Particle = sequence;
            return type;
        }

        [DebuggerNonUserCode]
        private void InitClass()
        {
            base.DataSetName = "DsSendList";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/DsSendList.xsd";
            base.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableV_List = new V_ListDataTable();
            base.Tables.Add(this.tableV_List);
            this.tableV_NP = new V_NPDataTable();
            base.Tables.Add(this.tableV_NP);
            this.tableV_SendList = new V_SendListDataTable();
            base.Tables.Add(this.tableV_SendList);
        }

        [DebuggerNonUserCode]
        protected override void InitializeDerivedDataSet()
        {
            base.BeginInit();
            this.InitClass();
            base.EndInit();
        }

        [DebuggerNonUserCode]
        internal void InitVars()
        {
            this.InitVars(true);
        }

        [DebuggerNonUserCode]
        internal void InitVars(bool initTable)
        {
            this.tableV_List = (V_ListDataTable) base.Tables["V_List"];
            if (initTable && (this.tableV_List != null))
            {
                this.tableV_List.InitVars();
            }
            this.tableV_NP = (V_NPDataTable) base.Tables["V_NP"];
            if (initTable && (this.tableV_NP != null))
            {
                this.tableV_NP.InitVars();
            }
            this.tableV_SendList = (V_SendListDataTable) base.Tables["V_SendList"];
            if (initTable && (this.tableV_SendList != null))
            {
                this.tableV_SendList.InitVars();
            }
        }

        [DebuggerNonUserCode]
        protected override void ReadXmlSerializable(XmlReader reader)
        {
            if (base.DetermineSchemaSerializationMode(reader) == System.Data.SchemaSerializationMode.IncludeSchema)
            {
                this.Reset();
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(reader);
                if (dataSet.Tables["V_List"] != null)
                {
                    base.Tables.Add(new V_ListDataTable(dataSet.Tables["V_List"]));
                }
                if (dataSet.Tables["V_NP"] != null)
                {
                    base.Tables.Add(new V_NPDataTable(dataSet.Tables["V_NP"]));
                }
                if (dataSet.Tables["V_SendList"] != null)
                {
                    base.Tables.Add(new V_SendListDataTable(dataSet.Tables["V_SendList"]));
                }
                base.DataSetName = dataSet.DataSetName;
                base.Prefix = dataSet.Prefix;
                base.Namespace = dataSet.Namespace;
                base.Locale = dataSet.Locale;
                base.CaseSensitive = dataSet.CaseSensitive;
                base.EnforceConstraints = dataSet.EnforceConstraints;
                base.Merge(dataSet, false, MissingSchemaAction.Add);
                this.InitVars();
            }
            else
            {
                base.ReadXml(reader);
                this.InitVars();
            }
        }

        [DebuggerNonUserCode]
        private void SchemaChanged(object sender, CollectionChangeEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Remove)
            {
                this.InitVars();
            }
        }

        [DebuggerNonUserCode]
        protected override bool ShouldSerializeRelations()
        {
            return false;
        }

        [DebuggerNonUserCode]
        protected override bool ShouldSerializeTables()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializeV_List()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializeV_NP()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializeV_SendList()
        {
            return false;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DebuggerNonUserCode]
        public DataRelationCollection Relations
        {
            get
            {
                return base.Relations;
            }
        }

        [DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Browsable(true)]
        public override System.Data.SchemaSerializationMode SchemaSerializationMode
        {
            get
            {
                return this._schemaSerializationMode;
            }
            set
            {
                this._schemaSerializationMode = value;
            }
        }

        [DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataTableCollection Tables
        {
            get
            {
                return base.Tables;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), DebuggerNonUserCode]
        public V_ListDataTable V_List
        {
            get
            {
                return this.tableV_List;
            }
        }

        [DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public V_NPDataTable V_NP
        {
            get
            {
                return this.tableV_NP;
            }
        }

        [Browsable(false), DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public V_SendListDataTable V_SendList
        {
            get
            {
                return this.tableV_SendList;
            }
        }

        [Serializable, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
        public class V_ListDataTable : DataTable, IEnumerable
        {
            private DataColumn columnCodeName;
            private DataColumn columnSendID;
            private DataColumn columnTotalNum;

            public event DsSendList.V_ListRowChangeEventHandler V_ListRowChanged;

            public event DsSendList.V_ListRowChangeEventHandler V_ListRowChanging;

            public event DsSendList.V_ListRowChangeEventHandler V_ListRowDeleted;

            public event DsSendList.V_ListRowChangeEventHandler V_ListRowDeleting;

            [DebuggerNonUserCode]
            public V_ListDataTable()
            {
                base.TableName = "V_List";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal V_ListDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode]
            protected V_ListDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddV_ListRow(DsSendList.V_ListRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public DsSendList.V_ListRow AddV_ListRow(string CodeName, string SendID, int TotalNum)
            {
                DsSendList.V_ListRow row = (DsSendList.V_ListRow) base.NewRow();
                row.ItemArray = new object[] { CodeName, SendID, TotalNum };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                DsSendList.V_ListDataTable table = (DsSendList.V_ListDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new DsSendList.V_ListDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(DsSendList.V_ListRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                DsSendList list = new DsSendList();
                xs.Add(list.GetSchemaSerializable());
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = 79228162514264337593543950335M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = list.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "V_ListDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columnCodeName = new DataColumn("CodeName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnCodeName);
                this.columnSendID = new DataColumn("SendID", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnSendID);
                this.columnTotalNum = new DataColumn("TotalNum", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnTotalNum);
                this.columnCodeName.MaxLength = 50;
                this.columnSendID.MaxLength = 50;
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columnCodeName = base.Columns["CodeName"];
                this.columnSendID = base.Columns["SendID"];
                this.columnTotalNum = base.Columns["TotalNum"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new DsSendList.V_ListRow(builder);
            }

            [DebuggerNonUserCode]
            public DsSendList.V_ListRow NewV_ListRow()
            {
                return (DsSendList.V_ListRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.V_ListRowChanged != null)
                {
                    this.V_ListRowChanged(this, new DsSendList.V_ListRowChangeEvent((DsSendList.V_ListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.V_ListRowChanging != null)
                {
                    this.V_ListRowChanging(this, new DsSendList.V_ListRowChangeEvent((DsSendList.V_ListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.V_ListRowDeleted != null)
                {
                    this.V_ListRowDeleted(this, new DsSendList.V_ListRowChangeEvent((DsSendList.V_ListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.V_ListRowDeleting != null)
                {
                    this.V_ListRowDeleting(this, new DsSendList.V_ListRowChangeEvent((DsSendList.V_ListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemoveV_ListRow(DsSendList.V_ListRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode]
            public DataColumn CodeNameColumn
            {
                get
                {
                    return this.columnCodeName;
                }
            }

            [DebuggerNonUserCode, Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            [DebuggerNonUserCode]
            public DsSendList.V_ListRow this[int index]
            {
                get
                {
                    return (DsSendList.V_ListRow) base.Rows[index];
                }
            }

            [DebuggerNonUserCode]
            public DataColumn SendIDColumn
            {
                get
                {
                    return this.columnSendID;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn TotalNumColumn
            {
                get
                {
                    return this.columnTotalNum;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_ListRow : DataRow
        {
            private DsSendList.V_ListDataTable tableV_List;

            [DebuggerNonUserCode]
            internal V_ListRow(DataRowBuilder rb) : base(rb)
            {
                this.tableV_List = (DsSendList.V_ListDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IsCodeNameNull()
            {
                return base.IsNull(this.tableV_List.CodeNameColumn);
            }

            [DebuggerNonUserCode]
            public bool IsSendIDNull()
            {
                return base.IsNull(this.tableV_List.SendIDColumn);
            }

            [DebuggerNonUserCode]
            public bool IsTotalNumNull()
            {
                return base.IsNull(this.tableV_List.TotalNumColumn);
            }

            [DebuggerNonUserCode]
            public void SetCodeNameNull()
            {
                base[this.tableV_List.CodeNameColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetSendIDNull()
            {
                base[this.tableV_List.SendIDColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetTotalNumNull()
            {
                base[this.tableV_List.TotalNumColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string CodeName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_List.CodeNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_List”中列“CodeName”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_List.CodeNameColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string SendID
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_List.SendIDColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_List”中列“SendID”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_List.SendIDColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int TotalNum
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableV_List.TotalNumColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_List”中列“TotalNum”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableV_List.TotalNumColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_ListRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private DsSendList.V_ListRow eventRow;

            [DebuggerNonUserCode]
            public V_ListRowChangeEvent(DsSendList.V_ListRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode]
            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            [DebuggerNonUserCode]
            public DsSendList.V_ListRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void V_ListRowChangeEventHandler(object sender, DsSendList.V_ListRowChangeEvent e);

        [Serializable, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
        public class V_NPDataTable : DataTable, IEnumerable
        {
            private DataColumn columnBarCode;
            private DataColumn columnCode;
            private DataColumn columnCodeName;
            private DataColumn columnDeadLine;
            private DataColumn columnNpNo;
            private DataColumn columnPlanID;
            private DataColumn columnPlanID1;
            private DataColumn columnPlanTime;
            private DataColumn columnSendID;
            private DataColumn columnTaskID;

            public event DsSendList.V_NPRowChangeEventHandler V_NPRowChanged;

            public event DsSendList.V_NPRowChangeEventHandler V_NPRowChanging;

            public event DsSendList.V_NPRowChangeEventHandler V_NPRowDeleted;

            public event DsSendList.V_NPRowChangeEventHandler V_NPRowDeleting;

            [DebuggerNonUserCode]
            public V_NPDataTable()
            {
                base.TableName = "V_NP";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal V_NPDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode]
            protected V_NPDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddV_NPRow(DsSendList.V_NPRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public DsSendList.V_NPRow AddV_NPRow(string NpNo, string Code, string CodeName, string PlanID, string PlanID1, string TaskID, string SendID, string BarCode, DateTime PlanTime, DateTime DeadLine)
            {
                DsSendList.V_NPRow row = (DsSendList.V_NPRow) base.NewRow();
                row.ItemArray = new object[] { NpNo, Code, CodeName, PlanID, PlanID1, TaskID, SendID, BarCode, PlanTime, DeadLine };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                DsSendList.V_NPDataTable table = (DsSendList.V_NPDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new DsSendList.V_NPDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(DsSendList.V_NPRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                DsSendList list = new DsSendList();
                xs.Add(list.GetSchemaSerializable());
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = 79228162514264337593543950335M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = list.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "V_NPDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columnNpNo = new DataColumn("NpNo", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnNpNo);
                this.columnCode = new DataColumn("Code", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnCode);
                this.columnCodeName = new DataColumn("CodeName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnCodeName);
                this.columnPlanID = new DataColumn("PlanID", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnPlanID);
                this.columnPlanID1 = new DataColumn("PlanID1", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnPlanID1);
                this.columnTaskID = new DataColumn("TaskID", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTaskID);
                this.columnSendID = new DataColumn("SendID", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnSendID);
                this.columnBarCode = new DataColumn("BarCode", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnBarCode);
                this.columnPlanTime = new DataColumn("PlanTime", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnPlanTime);
                this.columnDeadLine = new DataColumn("DeadLine", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnDeadLine);
                this.columnNpNo.AllowDBNull = false;
                this.columnNpNo.MaxLength = 20;
                this.columnCode.AllowDBNull = false;
                this.columnCode.MaxLength = 50;
                this.columnCodeName.MaxLength = 50;
                this.columnPlanID.AllowDBNull = false;
                this.columnPlanID.MaxLength = 50;
                this.columnPlanID1.MaxLength = 50;
                this.columnTaskID.MaxLength = 20;
                this.columnSendID.MaxLength = 50;
                this.columnBarCode.MaxLength = 20;
                this.columnPlanTime.AllowDBNull = false;
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columnNpNo = base.Columns["NpNo"];
                this.columnCode = base.Columns["Code"];
                this.columnCodeName = base.Columns["CodeName"];
                this.columnPlanID = base.Columns["PlanID"];
                this.columnPlanID1 = base.Columns["PlanID1"];
                this.columnTaskID = base.Columns["TaskID"];
                this.columnSendID = base.Columns["SendID"];
                this.columnBarCode = base.Columns["BarCode"];
                this.columnPlanTime = base.Columns["PlanTime"];
                this.columnDeadLine = base.Columns["DeadLine"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new DsSendList.V_NPRow(builder);
            }

            [DebuggerNonUserCode]
            public DsSendList.V_NPRow NewV_NPRow()
            {
                return (DsSendList.V_NPRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.V_NPRowChanged != null)
                {
                    this.V_NPRowChanged(this, new DsSendList.V_NPRowChangeEvent((DsSendList.V_NPRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.V_NPRowChanging != null)
                {
                    this.V_NPRowChanging(this, new DsSendList.V_NPRowChangeEvent((DsSendList.V_NPRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.V_NPRowDeleted != null)
                {
                    this.V_NPRowDeleted(this, new DsSendList.V_NPRowChangeEvent((DsSendList.V_NPRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.V_NPRowDeleting != null)
                {
                    this.V_NPRowDeleting(this, new DsSendList.V_NPRowChangeEvent((DsSendList.V_NPRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemoveV_NPRow(DsSendList.V_NPRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode]
            public DataColumn BarCodeColumn
            {
                get
                {
                    return this.columnBarCode;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn CodeColumn
            {
                get
                {
                    return this.columnCode;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn CodeNameColumn
            {
                get
                {
                    return this.columnCodeName;
                }
            }

            [DebuggerNonUserCode, Browsable(false)]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn DeadLineColumn
            {
                get
                {
                    return this.columnDeadLine;
                }
            }

            [DebuggerNonUserCode]
            public DsSendList.V_NPRow this[int index]
            {
                get
                {
                    return (DsSendList.V_NPRow) base.Rows[index];
                }
            }

            [DebuggerNonUserCode]
            public DataColumn NpNoColumn
            {
                get
                {
                    return this.columnNpNo;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PlanID1Column
            {
                get
                {
                    return this.columnPlanID1;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PlanIDColumn
            {
                get
                {
                    return this.columnPlanID;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PlanTimeColumn
            {
                get
                {
                    return this.columnPlanTime;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn SendIDColumn
            {
                get
                {
                    return this.columnSendID;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn TaskIDColumn
            {
                get
                {
                    return this.columnTaskID;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_NPRow : DataRow
        {
            private DsSendList.V_NPDataTable tableV_NP;

            [DebuggerNonUserCode]
            internal V_NPRow(DataRowBuilder rb) : base(rb)
            {
                this.tableV_NP = (DsSendList.V_NPDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IsBarCodeNull()
            {
                return base.IsNull(this.tableV_NP.BarCodeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsCodeNameNull()
            {
                return base.IsNull(this.tableV_NP.CodeNameColumn);
            }

            [DebuggerNonUserCode]
            public bool IsDeadLineNull()
            {
                return base.IsNull(this.tableV_NP.DeadLineColumn);
            }

            [DebuggerNonUserCode]
            public bool IsPlanID1Null()
            {
                return base.IsNull(this.tableV_NP.PlanID1Column);
            }

            [DebuggerNonUserCode]
            public bool IsSendIDNull()
            {
                return base.IsNull(this.tableV_NP.SendIDColumn);
            }

            [DebuggerNonUserCode]
            public bool IsTaskIDNull()
            {
                return base.IsNull(this.tableV_NP.TaskIDColumn);
            }

            [DebuggerNonUserCode]
            public void SetBarCodeNull()
            {
                base[this.tableV_NP.BarCodeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetCodeNameNull()
            {
                base[this.tableV_NP.CodeNameColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetDeadLineNull()
            {
                base[this.tableV_NP.DeadLineColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetPlanID1Null()
            {
                base[this.tableV_NP.PlanID1Column] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetSendIDNull()
            {
                base[this.tableV_NP.SendIDColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetTaskIDNull()
            {
                base[this.tableV_NP.TaskIDColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string BarCode
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_NP.BarCodeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_NP”中列“BarCode”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_NP.BarCodeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string Code
            {
                get
                {
                    return (string) base[this.tableV_NP.CodeColumn];
                }
                set
                {
                    base[this.tableV_NP.CodeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string CodeName
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_NP.CodeNameColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_NP”中列“CodeName”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_NP.CodeNameColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public DateTime DeadLine
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableV_NP.DeadLineColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_NP”中列“DeadLine”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableV_NP.DeadLineColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string NpNo
            {
                get
                {
                    return (string) base[this.tableV_NP.NpNoColumn];
                }
                set
                {
                    base[this.tableV_NP.NpNoColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string PlanID
            {
                get
                {
                    return (string) base[this.tableV_NP.PlanIDColumn];
                }
                set
                {
                    base[this.tableV_NP.PlanIDColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string PlanID1
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_NP.PlanID1Column];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_NP”中列“PlanID1”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_NP.PlanID1Column] = value;
                }
            }

            [DebuggerNonUserCode]
            public DateTime PlanTime
            {
                get
                {
                    return (DateTime) base[this.tableV_NP.PlanTimeColumn];
                }
                set
                {
                    base[this.tableV_NP.PlanTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string SendID
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_NP.SendIDColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_NP”中列“SendID”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_NP.SendIDColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string TaskID
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_NP.TaskIDColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_NP”中列“TaskID”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_NP.TaskIDColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_NPRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private DsSendList.V_NPRow eventRow;

            [DebuggerNonUserCode]
            public V_NPRowChangeEvent(DsSendList.V_NPRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode]
            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            [DebuggerNonUserCode]
            public DsSendList.V_NPRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void V_NPRowChangeEventHandler(object sender, DsSendList.V_NPRowChangeEvent e);

        [Serializable, XmlSchemaProvider("GetTypedTableSchema"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_SendListDataTable : DataTable, IEnumerable
        {
            private DataColumn columnPlanDepart;
            private DataColumn columnPlanId;
            private DataColumn columnReceivePerson;
            private DataColumn columnSendID;
            private DataColumn columnSendTime;
            private DataColumn columnSendUser;

            public event DsSendList.V_SendListRowChangeEventHandler V_SendListRowChanged;

            public event DsSendList.V_SendListRowChangeEventHandler V_SendListRowChanging;

            public event DsSendList.V_SendListRowChangeEventHandler V_SendListRowDeleted;

            public event DsSendList.V_SendListRowChangeEventHandler V_SendListRowDeleting;

            [DebuggerNonUserCode]
            public V_SendListDataTable()
            {
                base.TableName = "V_SendList";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal V_SendListDataTable(DataTable table)
            {
                base.TableName = table.TableName;
                if (table.CaseSensitive != table.DataSet.CaseSensitive)
                {
                    base.CaseSensitive = table.CaseSensitive;
                }
                if (table.Locale.ToString() != table.DataSet.Locale.ToString())
                {
                    base.Locale = table.Locale;
                }
                if (table.Namespace != table.DataSet.Namespace)
                {
                    base.Namespace = table.Namespace;
                }
                base.Prefix = table.Prefix;
                base.MinimumCapacity = table.MinimumCapacity;
            }

            [DebuggerNonUserCode]
            protected V_SendListDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddV_SendListRow(DsSendList.V_SendListRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public DsSendList.V_SendListRow AddV_SendListRow(string SendID, DateTime SendTime, string ReceivePerson, string PlanId, string PlanDepart, string SendUser)
            {
                DsSendList.V_SendListRow row = (DsSendList.V_SendListRow) base.NewRow();
                row.ItemArray = new object[] { SendID, SendTime, ReceivePerson, PlanId, PlanDepart, SendUser };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                DsSendList.V_SendListDataTable table = (DsSendList.V_SendListDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new DsSendList.V_SendListDataTable();
            }

            [DebuggerNonUserCode]
            public DsSendList.V_SendListRow FindBySendID(string SendID)
            {
                return (DsSendList.V_SendListRow) base.Rows.Find(new object[] { SendID });
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(DsSendList.V_SendListRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                DsSendList list = new DsSendList();
                xs.Add(list.GetSchemaSerializable());
                XmlSchemaAny item = new XmlSchemaAny {
                    Namespace = "http://www.w3.org/2001/XMLSchema",
                    MinOccurs = 0M,
                    MaxOccurs = 79228162514264337593543950335M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(item);
                XmlSchemaAny any2 = new XmlSchemaAny {
                    Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1",
                    MinOccurs = 1M,
                    ProcessContents = XmlSchemaContentProcessing.Lax
                };
                sequence.Items.Add(any2);
                XmlSchemaAttribute attribute = new XmlSchemaAttribute {
                    Name = "namespace",
                    FixedValue = list.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "V_SendListDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columnSendID = new DataColumn("SendID", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnSendID);
                this.columnSendTime = new DataColumn("SendTime", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnSendTime);
                this.columnReceivePerson = new DataColumn("ReceivePerson", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnReceivePerson);
                this.columnPlanId = new DataColumn("PlanId", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnPlanId);
                this.columnPlanDepart = new DataColumn("PlanDepart", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnPlanDepart);
                this.columnSendUser = new DataColumn("SendUser", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnSendUser);
                base.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] { this.columnSendID }, true));
                this.columnSendID.AllowDBNull = false;
                this.columnSendID.Unique = true;
                this.columnSendID.MaxLength = 50;
                this.columnReceivePerson.MaxLength = 50;
                this.columnPlanId.AllowDBNull = false;
                this.columnPlanId.MaxLength = 50;
                this.columnPlanDepart.MaxLength = 50;
                this.columnSendUser.MaxLength = 50;
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columnSendID = base.Columns["SendID"];
                this.columnSendTime = base.Columns["SendTime"];
                this.columnReceivePerson = base.Columns["ReceivePerson"];
                this.columnPlanId = base.Columns["PlanId"];
                this.columnPlanDepart = base.Columns["PlanDepart"];
                this.columnSendUser = base.Columns["SendUser"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new DsSendList.V_SendListRow(builder);
            }

            [DebuggerNonUserCode]
            public DsSendList.V_SendListRow NewV_SendListRow()
            {
                return (DsSendList.V_SendListRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.V_SendListRowChanged != null)
                {
                    this.V_SendListRowChanged(this, new DsSendList.V_SendListRowChangeEvent((DsSendList.V_SendListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.V_SendListRowChanging != null)
                {
                    this.V_SendListRowChanging(this, new DsSendList.V_SendListRowChangeEvent((DsSendList.V_SendListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.V_SendListRowDeleted != null)
                {
                    this.V_SendListRowDeleted(this, new DsSendList.V_SendListRowChangeEvent((DsSendList.V_SendListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.V_SendListRowDeleting != null)
                {
                    this.V_SendListRowDeleting(this, new DsSendList.V_SendListRowChangeEvent((DsSendList.V_SendListRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemoveV_SendListRow(DsSendList.V_SendListRow row)
            {
                base.Rows.Remove(row);
            }

            [Browsable(false), DebuggerNonUserCode]
            public int Count
            {
                get
                {
                    return base.Rows.Count;
                }
            }

            [DebuggerNonUserCode]
            public DsSendList.V_SendListRow this[int index]
            {
                get
                {
                    return (DsSendList.V_SendListRow) base.Rows[index];
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PlanDepartColumn
            {
                get
                {
                    return this.columnPlanDepart;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PlanIdColumn
            {
                get
                {
                    return this.columnPlanId;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn ReceivePersonColumn
            {
                get
                {
                    return this.columnReceivePerson;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn SendIDColumn
            {
                get
                {
                    return this.columnSendID;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn SendTimeColumn
            {
                get
                {
                    return this.columnSendTime;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn SendUserColumn
            {
                get
                {
                    return this.columnSendUser;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_SendListRow : DataRow
        {
            private DsSendList.V_SendListDataTable tableV_SendList;

            [DebuggerNonUserCode]
            internal V_SendListRow(DataRowBuilder rb) : base(rb)
            {
                this.tableV_SendList = (DsSendList.V_SendListDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IsPlanDepartNull()
            {
                return base.IsNull(this.tableV_SendList.PlanDepartColumn);
            }

            [DebuggerNonUserCode]
            public bool IsReceivePersonNull()
            {
                return base.IsNull(this.tableV_SendList.ReceivePersonColumn);
            }

            [DebuggerNonUserCode]
            public bool IsSendTimeNull()
            {
                return base.IsNull(this.tableV_SendList.SendTimeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsSendUserNull()
            {
                return base.IsNull(this.tableV_SendList.SendUserColumn);
            }

            [DebuggerNonUserCode]
            public void SetPlanDepartNull()
            {
                base[this.tableV_SendList.PlanDepartColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetReceivePersonNull()
            {
                base[this.tableV_SendList.ReceivePersonColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetSendTimeNull()
            {
                base[this.tableV_SendList.SendTimeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetSendUserNull()
            {
                base[this.tableV_SendList.SendUserColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public string PlanDepart
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_SendList.PlanDepartColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_SendList”中列“PlanDepart”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_SendList.PlanDepartColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string PlanId
            {
                get
                {
                    return (string) base[this.tableV_SendList.PlanIdColumn];
                }
                set
                {
                    base[this.tableV_SendList.PlanIdColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string ReceivePerson
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_SendList.ReceivePersonColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_SendList”中列“ReceivePerson”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_SendList.ReceivePersonColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string SendID
            {
                get
                {
                    return (string) base[this.tableV_SendList.SendIDColumn];
                }
                set
                {
                    base[this.tableV_SendList.SendIDColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public DateTime SendTime
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableV_SendList.SendTimeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_SendList”中列“SendTime”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableV_SendList.SendTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string SendUser
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_SendList.SendUserColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_SendList”中列“SendUser”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_SendList.SendUserColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_SendListRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private DsSendList.V_SendListRow eventRow;

            [DebuggerNonUserCode]
            public V_SendListRowChangeEvent(DsSendList.V_SendListRow row, DataRowAction action)
            {
                this.eventRow = row;
                this.eventAction = action;
            }

            [DebuggerNonUserCode]
            public DataRowAction Action
            {
                get
                {
                    return this.eventAction;
                }
            }

            [DebuggerNonUserCode]
            public DsSendList.V_SendListRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void V_SendListRowChangeEventHandler(object sender, DsSendList.V_SendListRowChangeEvent e);
    }
}

