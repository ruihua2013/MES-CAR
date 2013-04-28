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

    [Serializable, XmlRoot("DsTask"), HelpKeyword("vs.data.DataSet"), DesignerCategory("code"), GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), ToolboxItem(true), XmlSchemaProvider("GetTypedDataSetSchema")]
    public class DsTask : DataSet
    {
        private System.Data.SchemaSerializationMode _schemaSerializationMode;
        private V_NPDataTable tableV_NP;
        private V_TaskDataTable tableV_Task;

        [DebuggerNonUserCode]
        public DsTask()
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
        protected DsTask(SerializationInfo info, StreamingContext context) : base(info, context, false)
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
                    if (dataSet.Tables["V_Task"] != null)
                    {
                        base.Tables.Add(new V_TaskDataTable(dataSet.Tables["V_Task"]));
                    }
                    if (dataSet.Tables["V_NP"] != null)
                    {
                        base.Tables.Add(new V_NPDataTable(dataSet.Tables["V_NP"]));
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
            DsTask task = (DsTask) base.Clone();
            task.InitVars();
            task.SchemaSerializationMode = this.SchemaSerializationMode;
            return task;
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
            DsTask task = new DsTask();
            XmlSchemaComplexType type = new XmlSchemaComplexType();
            XmlSchemaSequence sequence = new XmlSchemaSequence();
            xs.Add(task.GetSchemaSerializable());
            XmlSchemaAny item = new XmlSchemaAny {
                Namespace = task.Namespace
            };
            sequence.Items.Add(item);
            type.Particle = sequence;
            return type;
        }

        [DebuggerNonUserCode]
        private void InitClass()
        {
            base.DataSetName = "DsTask";
            base.Prefix = "";
            base.Namespace = "http://tempuri.org/DsTask.xsd";
            base.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableV_Task = new V_TaskDataTable();
            base.Tables.Add(this.tableV_Task);
            this.tableV_NP = new V_NPDataTable();
            base.Tables.Add(this.tableV_NP);
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
            this.tableV_Task = (V_TaskDataTable) base.Tables["V_Task"];
            if (initTable && (this.tableV_Task != null))
            {
                this.tableV_Task.InitVars();
            }
            this.tableV_NP = (V_NPDataTable) base.Tables["V_NP"];
            if (initTable && (this.tableV_NP != null))
            {
                this.tableV_NP.InitVars();
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
                if (dataSet.Tables["V_Task"] != null)
                {
                    base.Tables.Add(new V_TaskDataTable(dataSet.Tables["V_Task"]));
                }
                if (dataSet.Tables["V_NP"] != null)
                {
                    base.Tables.Add(new V_NPDataTable(dataSet.Tables["V_NP"]));
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
        private bool ShouldSerializeV_NP()
        {
            return false;
        }

        [DebuggerNonUserCode]
        private bool ShouldSerializeV_Task()
        {
            return false;
        }

        [DebuggerNonUserCode, DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DebuggerNonUserCode]
        public DataTableCollection Tables
        {
            get
            {
                return base.Tables;
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

        [DebuggerNonUserCode, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public V_TaskDataTable V_Task
        {
            get
            {
                return this.tableV_Task;
            }
        }

        [Serializable, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
        public class V_NPDataTable : DataTable, IEnumerable
        {
            private DataColumn columnBarCode;
            private DataColumn columnCode;
            private DataColumn columnCodeName;
            private DataColumn columnDeadLine;
            private DataColumn columnDescription;
            private DataColumn columnNpNo;
            private DataColumn columnPlanID;
            private DataColumn columnPlanID1;
            private DataColumn columnPlanTime;
            private DataColumn columnSendID;
            private DataColumn columnTaskID;

            public event DsTask.V_NPRowChangeEventHandler V_NPRowChanged;

            public event DsTask.V_NPRowChangeEventHandler V_NPRowChanging;

            public event DsTask.V_NPRowChangeEventHandler V_NPRowDeleted;

            public event DsTask.V_NPRowChangeEventHandler V_NPRowDeleting;

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
            public void AddV_NPRow(DsTask.V_NPRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public DsTask.V_NPRow AddV_NPRow(string NpNo, string Code, string CodeName, string PlanID, string PlanID1, string TaskID, string SendID, string BarCode, DateTime PlanTime, DateTime DeadLine, string Description)
            {
                DsTask.V_NPRow row = (DsTask.V_NPRow) base.NewRow();
                row.ItemArray = new object[] { NpNo, Code, CodeName, PlanID, PlanID1, TaskID, SendID, BarCode, PlanTime, DeadLine, Description };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                DsTask.V_NPDataTable table = (DsTask.V_NPDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new DsTask.V_NPDataTable();
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(DsTask.V_NPRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                DsTask task = new DsTask();
                xs.Add(task.GetSchemaSerializable());
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
                    FixedValue = task.Namespace
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
                this.columnDescription = new DataColumn("Description", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnDescription);
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
                this.columnDescription.MaxLength = 200;
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
                this.columnDescription = base.Columns["Description"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new DsTask.V_NPRow(builder);
            }

            [DebuggerNonUserCode]
            public DsTask.V_NPRow NewV_NPRow()
            {
                return (DsTask.V_NPRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.V_NPRowChanged != null)
                {
                    this.V_NPRowChanged(this, new DsTask.V_NPRowChangeEvent((DsTask.V_NPRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.V_NPRowChanging != null)
                {
                    this.V_NPRowChanging(this, new DsTask.V_NPRowChangeEvent((DsTask.V_NPRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.V_NPRowDeleted != null)
                {
                    this.V_NPRowDeleted(this, new DsTask.V_NPRowChangeEvent((DsTask.V_NPRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.V_NPRowDeleting != null)
                {
                    this.V_NPRowDeleting(this, new DsTask.V_NPRowChangeEvent((DsTask.V_NPRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemoveV_NPRow(DsTask.V_NPRow row)
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

            [Browsable(false), DebuggerNonUserCode]
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
            public DataColumn DescriptionColumn
            {
                get
                {
                    return this.columnDescription;
                }
            }

            [DebuggerNonUserCode]
            public DsTask.V_NPRow this[int index]
            {
                get
                {
                    return (DsTask.V_NPRow) base.Rows[index];
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
            private DsTask.V_NPDataTable tableV_NP;

            [DebuggerNonUserCode]
            internal V_NPRow(DataRowBuilder rb) : base(rb)
            {
                this.tableV_NP = (DsTask.V_NPDataTable) base.Table;
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
            public bool IsDescriptionNull()
            {
                return base.IsNull(this.tableV_NP.DescriptionColumn);
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
            public void SetDescriptionNull()
            {
                base[this.tableV_NP.DescriptionColumn] = Convert.DBNull;
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
            public string Description
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_NP.DescriptionColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_NP”中列“Description”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_NP.DescriptionColumn] = value;
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
            private DsTask.V_NPRow eventRow;

            [DebuggerNonUserCode]
            public V_NPRowChangeEvent(DsTask.V_NPRow row, DataRowAction action)
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
            public DsTask.V_NPRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void V_NPRowChangeEventHandler(object sender, DsTask.V_NPRowChangeEvent e);

        [Serializable, GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0"), XmlSchemaProvider("GetTypedTableSchema")]
        public class V_TaskDataTable : DataTable, IEnumerable
        {
            private DataColumn columnClashTime;
            private DataColumn columnClashU;
            private DataColumn columnDryingTime;
            private DataColumn columnDryingU;
            private DataColumn columnEraseTime;
            private DataColumn columnEraseU;
            private DataColumn columnPackTime;
            private DataColumn columnPackU;
            private DataColumn columnPackUser;
            private DataColumn columnPasteTime;
            private DataColumn columnPasteU;
            private DataColumn columnPressTime;
            private DataColumn columnPressU;
            private DataColumn columnSendTime;
            private DataColumn columnSendU;
            private DataColumn columnTaskID;
            private DataColumn columnTaskTime;
            private DataColumn columnTaskUser;
            private DataColumn columnUserName;
            private DataColumn columnWashTime;
            private DataColumn columnWashU;

            public event DsTask.V_TaskRowChangeEventHandler V_TaskRowChanged;

            public event DsTask.V_TaskRowChangeEventHandler V_TaskRowChanging;

            public event DsTask.V_TaskRowChangeEventHandler V_TaskRowDeleted;

            public event DsTask.V_TaskRowChangeEventHandler V_TaskRowDeleting;

            [DebuggerNonUserCode]
            public V_TaskDataTable()
            {
                base.TableName = "V_Task";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }

            [DebuggerNonUserCode]
            internal V_TaskDataTable(DataTable table)
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
            protected V_TaskDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
            {
                this.InitVars();
            }

            [DebuggerNonUserCode]
            public void AddV_TaskRow(DsTask.V_TaskRow row)
            {
                base.Rows.Add(row);
            }

            [DebuggerNonUserCode]
            public DsTask.V_TaskRow AddV_TaskRow(string UserName, string TaskID, DateTime TaskTime, int TaskUser, int PressU, int EraseU, int DryingU, int WashU, int PasteU, int ClashU, int PackU, int SendU, DateTime PressTime, DateTime EraseTime, DateTime DryingTime, DateTime WashTime, DateTime PasteTime, DateTime ClashTime, DateTime PackTime, DateTime SendTime, string PackUser)
            {
                DsTask.V_TaskRow row = (DsTask.V_TaskRow) base.NewRow();
                row.ItemArray = new object[] { 
                    UserName, TaskID, TaskTime, TaskUser, PressU, EraseU, DryingU, WashU, PasteU, ClashU, PackU, SendU, PressTime, EraseTime, DryingTime, WashTime, 
                    PasteTime, ClashTime, PackTime, SendTime, PackUser
                 };
                base.Rows.Add(row);
                return row;
            }

            [DebuggerNonUserCode]
            public override DataTable Clone()
            {
                DsTask.V_TaskDataTable table = (DsTask.V_TaskDataTable) base.Clone();
                table.InitVars();
                return table;
            }

            [DebuggerNonUserCode]
            protected override DataTable CreateInstance()
            {
                return new DsTask.V_TaskDataTable();
            }

            [DebuggerNonUserCode]
            public DsTask.V_TaskRow FindByTaskID(string TaskID)
            {
                return (DsTask.V_TaskRow) base.Rows.Find(new object[] { TaskID });
            }

            [DebuggerNonUserCode]
            public virtual IEnumerator GetEnumerator()
            {
                return base.Rows.GetEnumerator();
            }

            [DebuggerNonUserCode]
            protected override Type GetRowType()
            {
                return typeof(DsTask.V_TaskRow);
            }

            [DebuggerNonUserCode]
            public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
            {
                XmlSchemaComplexType type = new XmlSchemaComplexType();
                XmlSchemaSequence sequence = new XmlSchemaSequence();
                DsTask task = new DsTask();
                xs.Add(task.GetSchemaSerializable());
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
                    FixedValue = task.Namespace
                };
                type.Attributes.Add(attribute);
                XmlSchemaAttribute attribute2 = new XmlSchemaAttribute {
                    Name = "tableTypeName",
                    FixedValue = "V_TaskDataTable"
                };
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }

            [DebuggerNonUserCode]
            private void InitClass()
            {
                this.columnUserName = new DataColumn("UserName", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnUserName);
                this.columnTaskID = new DataColumn("TaskID", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnTaskID);
                this.columnTaskTime = new DataColumn("TaskTime", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnTaskTime);
                this.columnTaskUser = new DataColumn("TaskUser", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnTaskUser);
                this.columnPressU = new DataColumn("PressU", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnPressU);
                this.columnEraseU = new DataColumn("EraseU", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnEraseU);
                this.columnDryingU = new DataColumn("DryingU", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnDryingU);
                this.columnWashU = new DataColumn("WashU", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnWashU);
                this.columnPasteU = new DataColumn("PasteU", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnPasteU);
                this.columnClashU = new DataColumn("ClashU", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnClashU);
                this.columnPackU = new DataColumn("PackU", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnPackU);
                this.columnSendU = new DataColumn("SendU", typeof(int), null, MappingType.Element);
                base.Columns.Add(this.columnSendU);
                this.columnPressTime = new DataColumn("PressTime", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnPressTime);
                this.columnEraseTime = new DataColumn("EraseTime", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnEraseTime);
                this.columnDryingTime = new DataColumn("DryingTime", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnDryingTime);
                this.columnWashTime = new DataColumn("WashTime", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnWashTime);
                this.columnPasteTime = new DataColumn("PasteTime", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnPasteTime);
                this.columnClashTime = new DataColumn("ClashTime", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnClashTime);
                this.columnPackTime = new DataColumn("PackTime", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnPackTime);
                this.columnSendTime = new DataColumn("SendTime", typeof(DateTime), null, MappingType.Element);
                base.Columns.Add(this.columnSendTime);
                this.columnPackUser = new DataColumn("PackUser", typeof(string), null, MappingType.Element);
                base.Columns.Add(this.columnPackUser);
                base.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] { this.columnTaskID }, true));
                this.columnUserName.AllowDBNull = false;
                this.columnUserName.MaxLength = 50;
                this.columnTaskID.AllowDBNull = false;
                this.columnTaskID.Unique = true;
                this.columnTaskID.MaxLength = 20;
                this.columnPackUser.MaxLength = 50;
            }

            [DebuggerNonUserCode]
            internal void InitVars()
            {
                this.columnUserName = base.Columns["UserName"];
                this.columnTaskID = base.Columns["TaskID"];
                this.columnTaskTime = base.Columns["TaskTime"];
                this.columnTaskUser = base.Columns["TaskUser"];
                this.columnPressU = base.Columns["PressU"];
                this.columnEraseU = base.Columns["EraseU"];
                this.columnDryingU = base.Columns["DryingU"];
                this.columnWashU = base.Columns["WashU"];
                this.columnPasteU = base.Columns["PasteU"];
                this.columnClashU = base.Columns["ClashU"];
                this.columnPackU = base.Columns["PackU"];
                this.columnSendU = base.Columns["SendU"];
                this.columnPressTime = base.Columns["PressTime"];
                this.columnEraseTime = base.Columns["EraseTime"];
                this.columnDryingTime = base.Columns["DryingTime"];
                this.columnWashTime = base.Columns["WashTime"];
                this.columnPasteTime = base.Columns["PasteTime"];
                this.columnClashTime = base.Columns["ClashTime"];
                this.columnPackTime = base.Columns["PackTime"];
                this.columnSendTime = base.Columns["SendTime"];
                this.columnPackUser = base.Columns["PackUser"];
            }

            [DebuggerNonUserCode]
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
            {
                return new DsTask.V_TaskRow(builder);
            }

            [DebuggerNonUserCode]
            public DsTask.V_TaskRow NewV_TaskRow()
            {
                return (DsTask.V_TaskRow) base.NewRow();
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanged(DataRowChangeEventArgs e)
            {
                base.OnRowChanged(e);
                if (this.V_TaskRowChanged != null)
                {
                    this.V_TaskRowChanged(this, new DsTask.V_TaskRowChangeEvent((DsTask.V_TaskRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowChanging(DataRowChangeEventArgs e)
            {
                base.OnRowChanging(e);
                if (this.V_TaskRowChanging != null)
                {
                    this.V_TaskRowChanging(this, new DsTask.V_TaskRowChangeEvent((DsTask.V_TaskRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleted(DataRowChangeEventArgs e)
            {
                base.OnRowDeleted(e);
                if (this.V_TaskRowDeleted != null)
                {
                    this.V_TaskRowDeleted(this, new DsTask.V_TaskRowChangeEvent((DsTask.V_TaskRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            protected override void OnRowDeleting(DataRowChangeEventArgs e)
            {
                base.OnRowDeleting(e);
                if (this.V_TaskRowDeleting != null)
                {
                    this.V_TaskRowDeleting(this, new DsTask.V_TaskRowChangeEvent((DsTask.V_TaskRow) e.Row, e.Action));
                }
            }

            [DebuggerNonUserCode]
            public void RemoveV_TaskRow(DsTask.V_TaskRow row)
            {
                base.Rows.Remove(row);
            }

            [DebuggerNonUserCode]
            public DataColumn ClashTimeColumn
            {
                get
                {
                    return this.columnClashTime;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn ClashUColumn
            {
                get
                {
                    return this.columnClashU;
                }
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
            public DataColumn DryingTimeColumn
            {
                get
                {
                    return this.columnDryingTime;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn DryingUColumn
            {
                get
                {
                    return this.columnDryingU;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn EraseTimeColumn
            {
                get
                {
                    return this.columnEraseTime;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn EraseUColumn
            {
                get
                {
                    return this.columnEraseU;
                }
            }

            [DebuggerNonUserCode]
            public DsTask.V_TaskRow this[int index]
            {
                get
                {
                    return (DsTask.V_TaskRow) base.Rows[index];
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PackTimeColumn
            {
                get
                {
                    return this.columnPackTime;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PackUColumn
            {
                get
                {
                    return this.columnPackU;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PackUserColumn
            {
                get
                {
                    return this.columnPackUser;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PasteTimeColumn
            {
                get
                {
                    return this.columnPasteTime;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PasteUColumn
            {
                get
                {
                    return this.columnPasteU;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PressTimeColumn
            {
                get
                {
                    return this.columnPressTime;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn PressUColumn
            {
                get
                {
                    return this.columnPressU;
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
            public DataColumn SendUColumn
            {
                get
                {
                    return this.columnSendU;
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

            [DebuggerNonUserCode]
            public DataColumn TaskTimeColumn
            {
                get
                {
                    return this.columnTaskTime;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn TaskUserColumn
            {
                get
                {
                    return this.columnTaskUser;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn UserNameColumn
            {
                get
                {
                    return this.columnUserName;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn WashTimeColumn
            {
                get
                {
                    return this.columnWashTime;
                }
            }

            [DebuggerNonUserCode]
            public DataColumn WashUColumn
            {
                get
                {
                    return this.columnWashU;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_TaskRow : DataRow
        {
            private DsTask.V_TaskDataTable tableV_Task;

            [DebuggerNonUserCode]
            internal V_TaskRow(DataRowBuilder rb) : base(rb)
            {
                this.tableV_Task = (DsTask.V_TaskDataTable) base.Table;
            }

            [DebuggerNonUserCode]
            public bool IsClashTimeNull()
            {
                return base.IsNull(this.tableV_Task.ClashTimeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsClashUNull()
            {
                return base.IsNull(this.tableV_Task.ClashUColumn);
            }

            [DebuggerNonUserCode]
            public bool IsDryingTimeNull()
            {
                return base.IsNull(this.tableV_Task.DryingTimeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsDryingUNull()
            {
                return base.IsNull(this.tableV_Task.DryingUColumn);
            }

            [DebuggerNonUserCode]
            public bool IsEraseTimeNull()
            {
                return base.IsNull(this.tableV_Task.EraseTimeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsEraseUNull()
            {
                return base.IsNull(this.tableV_Task.EraseUColumn);
            }

            [DebuggerNonUserCode]
            public bool IsPackTimeNull()
            {
                return base.IsNull(this.tableV_Task.PackTimeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsPackUNull()
            {
                return base.IsNull(this.tableV_Task.PackUColumn);
            }

            [DebuggerNonUserCode]
            public bool IsPackUserNull()
            {
                return base.IsNull(this.tableV_Task.PackUserColumn);
            }

            [DebuggerNonUserCode]
            public bool IsPasteTimeNull()
            {
                return base.IsNull(this.tableV_Task.PasteTimeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsPasteUNull()
            {
                return base.IsNull(this.tableV_Task.PasteUColumn);
            }

            [DebuggerNonUserCode]
            public bool IsPressTimeNull()
            {
                return base.IsNull(this.tableV_Task.PressTimeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsPressUNull()
            {
                return base.IsNull(this.tableV_Task.PressUColumn);
            }

            [DebuggerNonUserCode]
            public bool IsSendTimeNull()
            {
                return base.IsNull(this.tableV_Task.SendTimeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsSendUNull()
            {
                return base.IsNull(this.tableV_Task.SendUColumn);
            }

            [DebuggerNonUserCode]
            public bool IsTaskTimeNull()
            {
                return base.IsNull(this.tableV_Task.TaskTimeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsTaskUserNull()
            {
                return base.IsNull(this.tableV_Task.TaskUserColumn);
            }

            [DebuggerNonUserCode]
            public bool IsWashTimeNull()
            {
                return base.IsNull(this.tableV_Task.WashTimeColumn);
            }

            [DebuggerNonUserCode]
            public bool IsWashUNull()
            {
                return base.IsNull(this.tableV_Task.WashUColumn);
            }

            [DebuggerNonUserCode]
            public void SetClashTimeNull()
            {
                base[this.tableV_Task.ClashTimeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetClashUNull()
            {
                base[this.tableV_Task.ClashUColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetDryingTimeNull()
            {
                base[this.tableV_Task.DryingTimeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetDryingUNull()
            {
                base[this.tableV_Task.DryingUColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetEraseTimeNull()
            {
                base[this.tableV_Task.EraseTimeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetEraseUNull()
            {
                base[this.tableV_Task.EraseUColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetPackTimeNull()
            {
                base[this.tableV_Task.PackTimeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetPackUNull()
            {
                base[this.tableV_Task.PackUColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetPackUserNull()
            {
                base[this.tableV_Task.PackUserColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetPasteTimeNull()
            {
                base[this.tableV_Task.PasteTimeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetPasteUNull()
            {
                base[this.tableV_Task.PasteUColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetPressTimeNull()
            {
                base[this.tableV_Task.PressTimeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetPressUNull()
            {
                base[this.tableV_Task.PressUColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetSendTimeNull()
            {
                base[this.tableV_Task.SendTimeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetSendUNull()
            {
                base[this.tableV_Task.SendUColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetTaskTimeNull()
            {
                base[this.tableV_Task.TaskTimeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetTaskUserNull()
            {
                base[this.tableV_Task.TaskUserColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetWashTimeNull()
            {
                base[this.tableV_Task.WashTimeColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public void SetWashUNull()
            {
                base[this.tableV_Task.WashUColumn] = Convert.DBNull;
            }

            [DebuggerNonUserCode]
            public DateTime ClashTime
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableV_Task.ClashTimeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“ClashTime”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableV_Task.ClashTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int ClashU
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableV_Task.ClashUColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“ClashU”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableV_Task.ClashUColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public DateTime DryingTime
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableV_Task.DryingTimeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“DryingTime”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableV_Task.DryingTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int DryingU
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableV_Task.DryingUColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“DryingU”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableV_Task.DryingUColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public DateTime EraseTime
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableV_Task.EraseTimeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“EraseTime”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableV_Task.EraseTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int EraseU
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableV_Task.EraseUColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“EraseU”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableV_Task.EraseUColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public DateTime PackTime
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableV_Task.PackTimeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“PackTime”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableV_Task.PackTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int PackU
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableV_Task.PackUColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“PackU”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableV_Task.PackUColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string PackUser
            {
                get
                {
                    string str;
                    try
                    {
                        str = (string) base[this.tableV_Task.PackUserColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“PackUser”的值为 DBNull。", exception);
                    }
                    return str;
                }
                set
                {
                    base[this.tableV_Task.PackUserColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public DateTime PasteTime
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableV_Task.PasteTimeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“PasteTime”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableV_Task.PasteTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int PasteU
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableV_Task.PasteUColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“PasteU”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableV_Task.PasteUColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public DateTime PressTime
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableV_Task.PressTimeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“PressTime”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableV_Task.PressTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int PressU
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableV_Task.PressUColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“PressU”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableV_Task.PressUColumn] = value;
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
                        time = (DateTime) base[this.tableV_Task.SendTimeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“SendTime”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableV_Task.SendTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int SendU
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableV_Task.SendUColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“SendU”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableV_Task.SendUColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string TaskID
            {
                get
                {
                    return (string) base[this.tableV_Task.TaskIDColumn];
                }
                set
                {
                    base[this.tableV_Task.TaskIDColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public DateTime TaskTime
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableV_Task.TaskTimeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“TaskTime”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableV_Task.TaskTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int TaskUser
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableV_Task.TaskUserColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“TaskUser”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableV_Task.TaskUserColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public string UserName
            {
                get
                {
                    return (string) base[this.tableV_Task.UserNameColumn];
                }
                set
                {
                    base[this.tableV_Task.UserNameColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public DateTime WashTime
            {
                get
                {
                    DateTime time;
                    try
                    {
                        time = (DateTime) base[this.tableV_Task.WashTimeColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“WashTime”的值为 DBNull。", exception);
                    }
                    return time;
                }
                set
                {
                    base[this.tableV_Task.WashTimeColumn] = value;
                }
            }

            [DebuggerNonUserCode]
            public int WashU
            {
                get
                {
                    int num;
                    try
                    {
                        num = (int) base[this.tableV_Task.WashUColumn];
                    }
                    catch (InvalidCastException exception)
                    {
                        throw new StrongTypingException("表“V_Task”中列“WashU”的值为 DBNull。", exception);
                    }
                    return num;
                }
                set
                {
                    base[this.tableV_Task.WashUColumn] = value;
                }
            }
        }

        [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class V_TaskRowChangeEvent : EventArgs
        {
            private DataRowAction eventAction;
            private DsTask.V_TaskRow eventRow;

            [DebuggerNonUserCode]
            public V_TaskRowChangeEvent(DsTask.V_TaskRow row, DataRowAction action)
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
            public DsTask.V_TaskRow Row
            {
                get
                {
                    return this.eventRow;
                }
            }
        }

        public delegate void V_TaskRowChangeEventHandler(object sender, DsTask.V_TaskRowChangeEvent e);
    }
}

