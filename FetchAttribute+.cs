namespace FetchXmlWrapper
{
    //Generic additions
    public partial class FetchAttribute

    {
        #region Aron Fischman Added on 11/09/2015

        private bool distinctField;
        private bool distinctFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool distinct
        {
            get
            {
                return distinctField;
            }
            set
            {
                distinctField = value;
                distinctFieldSpecified = true;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool distinctSpecified
        {
            get
            {
                return distinctFieldSpecified;
            }
            set
            {
                distinctFieldSpecified = value;
            }
        }
        #endregion
    }
}
