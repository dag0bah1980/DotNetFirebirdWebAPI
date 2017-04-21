using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JSONFirebirdWebServiceTest.Definitions
{
    public partial class Definitions : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.PageTitleLiteral.Text = PageTitle;
        }

        void Page_Init(Object sender, EventArgs e)
        {

        }

        public String PageTitle
        {
            get { return (String)ViewState["ptitle"]; }
            set { ViewState["ptitle"] = value; }
        }
    }
}