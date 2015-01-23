using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DL;
using BLL;
using System.Data;
using System.Net.Mail;
using SigmaUtils;
using System.Configuration;

public partial class Company_Application_Form : System.Web.UI.Page
{
    New_Application_Form_DL Form_DL = new New_Application_Form_DL();
    New_Application_Form_BLL Form_BLL = new New_Application_Form_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        chkotherTitle.Attributes.Add("onclick", "radioMe(event);");
        ChkStatus.Attributes.Add("onclick", "radioMe1(event);");
        chkSource.Attributes.Add("onclick", "radioMe2(event);");
        txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        if (!IsPostBack)
        {
            Day();
            Year();
        }
    }

    public void Day()
    {
        for (int i = 1; i <= 31; i++)
        {
            ddlday.Items.Add(i.ToString());
        }
        ddlday.Items.Insert(0, "Day");
    }

    public void Year()
    {
        int year1 = System.DateTime.Now.Year;

        for (int i = year1; i >= 1900; i--)
        {
            ddlYear.Items.Add(i.ToString());
        }

        ddlYear.Items.Insert(0, "Year");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (chkTerms.Checked)
        {

            string s2 = "";
            for (int i = 0; i < chkotherTitle.Items.Count; i++)
            {
                if (chkotherTitle.Items[i].Selected)
                {
                    s2 = chkotherTitle.SelectedItem.Text;
                }

            }
            Form_DL.New_Title = s2;
        Form_DL.First_Name = txtFirstName.Text.ToString().Trim();
        Form_DL.Last_Name = txtLastName.Text.ToString().Trim();
        Form_DL.Middle_Name = txtMiddleName.Text.ToString().Trim();
        Form_DL.Printed_Name = txtPrintName.Text.ToString().Trim();
        Form_DL.Gender ="";
        string date1 = ddlday.SelectedItem.Value + "/" + ddlMonth.SelectedItem.Value + "/" + ddlYear.SelectedItem.Value;

        Form_DL.DOB = Convert.ToDateTime(date(date1));

        Form_DL.Father_Last_Name = txtFatherLastname.Text.ToString().Trim();
        Form_DL.Father_First_Name = txtFatherFirstname.Text.ToString().Trim();
        Form_DL.Father_Middle_Name = txtFathermiddlename.Text.ToString().Trim();

        Form_DL.Residence_Address= "";
        Form_DL.Residence_Address_State = txtRESState.Text.ToString();
        Form_DL.Residence_Flat = txtResFlat.Text.ToString().Trim();
        Form_DL.Residence_Buliding = txtBuliding.Text.ToString().Trim();
        Form_DL.Residence_Address_Post = txtResStreet.Text.ToString().Trim();
        Form_DL.Residence_Address_Area = txtResArea.Text.ToString().Trim();
        Form_DL.Residence_Address_City = txtResTown.Text.ToString().Trim();
        Form_DL.Residence_Address_Pincode = txtResPincode.Text.ToString().Trim();
        Form_DL.Residence_Address_Country_Name = txtResCountry.Text.ToString().Trim();

        Form_DL.Office_Name = txtOfficeName.Text.ToString().Trim();
        Form_DL.Office_Flat = txtOfficeFlat.Text.ToString().Trim();
        Form_DL.Office_Buliding = txtOfficeBuliding.Text.ToString().Trim();
        Form_DL.Office_Address_Post = txtOfficeStreet.Text.ToString().Trim();
        Form_DL.Office_Address_Area = txtOfficeLocality.Text.ToString().Trim();
        Form_DL.Office_Address_City = txtOfficeCity.Text.ToString().Trim();
        Form_DL.Office_Address_Pincode = txtOfficePincode.Text.ToString().Trim();
        Form_DL.Office_Address_Country_Name = txtOfficeCountry.Text.ToString().Trim();
        if (chkAddress != null)
        {
            string s = "";

            for (int i = 0; i < chkAddress.Items.Count; i++)
            {
                if (chkAddress.Items[i].Selected)
                {
                    s = chkAddress.Items[i].Value;
                }
            }

            Form_DL.Address_Communication = s;
        }
        else
        {
            Form_DL.Address_Communication = "";
        }

        Form_DL.Telephone_Area_Code = txtAreacode.Text.ToString().Trim();
        Form_DL.Telephone_Country_Code = txtCountrycode.Text.ToString().Trim();
        Form_DL.Telephone_Telephone_No = txtPhoneno.Text.ToString().Trim();

        Form_DL.Emaill_Id = txtemailid.Text.ToString().Trim();



        if (CheckBox1.Checked)
        {
            Form_DL.Satus_Of_Applicant = "Yes";
            string s = "";
            for (int i = 0; i < ChkStatus.Items.Count; i++)
            {
                if (ChkStatus.Items[i].Selected)
                {
                    s = ChkStatus.SelectedItem.Text;

                }

            }
            Form_DL.Satus_Applicant = s;

        }
        else
        {
            Form_DL.Satus_Of_Applicant = "No";
            Form_DL.Satus_Applicant = "";
        }



        Form_DL.Registration_Number = txtRegistration.Text.ToString().Trim();
        Form_DL.AADHAAR_No = txtAAdhaar.Text.ToString().Trim();
        if (CheckBox2.Checked)
        {
            Form_DL.Source_Of_Income = "Yes";
            string s = "";
            for (int i = 0; i < chkSource.Items.Count; i++)
            {
                if (chkSource.Items[i].Selected)
                {
                    s = chkSource.SelectedItem.Text;
                }
            }
            Form_DL.Source_Income = s;

        }
        else
        {
            Form_DL.Source_Of_Income = "No";
            Form_DL.Source_Income = "";
        }


        Form_DL.Proof_Of_Identity = ddlProofofAddress.SelectedItem.Text;
        Form_DL.Proof_Of_Address = ddlPOI.SelectedItem.Text;
        Form_DL.Residence_Address_State = txtRESState.Text.ToString().Trim();
        Form_DL.Office_Address_State = txtofficestate.Text.ToString().Trim();

        Form_DL.New_we = txtname1.Text.ToString().Trim();
        Form_DL.New_Capacity = ddlCapacity.SelectedItem.Text;
        Form_DL.New_Place = txtPlace.Text.ToString().Trim();
        Form_DL.New_Date = Convert.ToDateTime(date(txtDate.Text));
        string conn = ConfigurationManager.ConnectionStrings["ConnString"].ToString();

        DataTable dtemail = Form_BLL.User_Registeration_Select_username(txtemailid.Text);

        string password = "";
        if (dtemail.Rows.Count > 0)
        {
            Form_DL.Password = "";
            Form_DL.Username = "";
        }
        else
        {

            dtemail = DBWizard.FillDataTable(conn, "Sp_Select_NRI", new object[] { txtemailid.Text });
            if (dtemail.Rows.Count > 0)
            {
                Form_DL.Password = "";
                Form_DL.Username = "";
            }
            else
            {
                dtemail = DBWizard.FillDataTable(conn, "Sp_Select_Correction_Pan_Card_Email_Id", new object[] { txtemailid.Text });

                if (dtemail.Rows.Count > 0)
                {
                    Form_DL.Password = dtemail.Rows[0]["Correction_Password"].ToString();
                    Form_DL.Username = "";
                }
                else
                {
                    password = GeneratePassword();

                    Form_DL.Password = password;
                    Form_DL.Username = txtemailid.Text;
                }
            }
        }
        Form_DL.Proof_Of_DOB = "";
        int id = Form_BLL.Insert_Tb_Correction_PAN(Form_DL);

        Session["DL1"] = Form_DL;
         if (id > 0)
            {

                

                if (password != "")
                {
                    Response.Write("<script>alert('New PAN SAVED successfully')</script>");
                  
                    Clear();

                    Response.Redirect("http://pancardgetit.com/Registeration/default.aspx");


                }
                else
                {
                    Response.Redirect("http://pancardgetit.com/Registeration/default.aspx");
                }

            }
            else
            {
                Response.Write("<script>alert('New PAN Fail T SAVED')</script>");
                Clear();
            }
        }
        else
        {
            Response.Write("<script>alert('Please Accept Terms And Conditions')</script>");
        }

    }
    public void User_Mail(string password, string Username)
    {
        string Str = "";
        Str += "<table border='0'style='font-family:Verdana;font-size:15px;color=#003399;padding-left:4px;'>";
        Str += "<tr><td colspan='3'>Dear Pan Applicant we have just received your Pan Application</td></tr>";
        Str += "<tr><td colspan='3'>&nbsp;</td></tr>";
        Str += "Your account is Deatils are ";
        Str += "<tr><td colspan='3'>Please login through this link <a href='https://pancardgetit.com/Registeration/user_login.aspx'>Login</a> and use below password </td></tr>";
        Str += "<tr><td colspan='3'>&nbsp;</td></tr>";
        Str += "<tr><td colspan='3'><b>user Name</b> : " + Username + "</td></tr>";
        Str += "<tr><td colspan='3'>&nbsp;</td></tr>";
        Str += "<tr><td colspan='3'><b>Password</b> :" + password + "</td></tr>";

        Str += "<tr><td colspan='3'>we welcome you to the Pancardgetit.com community. We have got more than 10000 customers on-board and with you joining I hope that the community grows further strong.</td></tr>";
        Str += "<tr><td colspan='3'>What's next:</td></tr>";
        Str += "<tr><td colspan='3'>1)	go to www.pancardgetit.com</td></tr>";
        Str += "<tr><td colspan='3'>2)	login with your user name & password</td></tr>";
        Str += "<tr><td colspan='3'>3)	Download &Take Print-out of the application.</td></tr>";

        Str += "<tr><td colspan='3'>4)	Any applicant wrongly filled any detail in application form while applying online, then the applicant can correct the detail by using whitener</td></tr>";
        Str += "<tr><td colspan='3'>5)	Two colored photos(only in case of individuals) should be pasted/affixed (strapless) in the two boxes provided at top of the application (<b>see Sample Application form section</b>).</td></tr>";
        Str += "<tr><td colspan='3'>6)	Make 3 signatures in <b>Blank Ink</b> (see Sample Form on website) as below :-</td></tr>";
        Str += "<tr><td colspan='3'>7)	In the box below the right hand side photo (Nothing on Photo).</td></tr>";
        Str += "<tr><td colspan='3'>8)	Across the Left hand side photo (partialy on paper and partially on photo).</td></tr>";
        Str += "<tr><td colspan='3'>9)	In the Bottom Box in right hand side</td></tr>";
        Str += "<tr><td colspan='3'>10)	Name and father's name should exactly match with identity proof such as Driving license or Passport or Other proofs as per list of Identity Proofs</td></tr>";
        Str += "<tr><td colspan='3'>11)	In case of minor, application should be signed by guardian only with the photos of minor and only ID of the guardian should be required.</td></tr>";

        Str += "<tr><td colspan='3'>12)	<b>Upload</b> scanned Copy of application complete in all respect (2 photos and 1+2 signatures in BLACK INK) along with copy of proof/s of identity, address and date of birth(For all Proof SELF-ATTESTATION (signature) is Mandatory )  in Upload Section(respectively)  in your account  within 24hours & send us confirmation mail to support@ipancardgetit.com & Send hard copy of application complete in all respect (2 photos and 1+2 signatures in BLACK INK) along with copy of proof/s of identity, address and date of birth by speed/registered post a post(preferably)/ reliable courier Make Sure that it reaches within 7days at following address :-</td></tr>";

        Str += "<tr><td colspan='3'><b>PanCardgetit.com</b></td></tr>";
        Str += "<tr><td colspan='3'><b>C/o SAFINDIA Premises</b></td></tr>";
        Str += "<tr><td colspan='3'><b>#2 ,2nd Flr ,NCR COMPLEX,</b></td></tr>";
        Str += "<tr><td colspan='3'><b>T.C.PALYA Main ,NEAR ST.Anthony Church</b></td></tr>";
        Str += "<tr><td colspan='3'><b>K.R.Puram(Post) Bangalore-560036 (INDIA)</b></td></tr>";
        Str += "<tr><td colspan='3'><b>Ph no:08060509700</b></td></tr>";
        Str += "</table>";

        sendmail(Str, txtemailid.Text);
    }
    public void sendmail(string body, string ToAddress)
    {

        System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
        System.Net.Mail.SmtpClient smtpClient = new SmtpClient();
        string msg = string.Empty;
        try
        {


            MailAddress fromAddress = new MailAddress("info@pancardgetit.com");
            //add from address
            message.From = fromAddress;
            //add to address
            message.To.Add(ToAddress);
            //message.CC.Add("balki@softwaremadeeasy.biz");
            message.Subject = "Your Login information- Important ";
            message.IsBodyHtml = true;
            message.Body = body;
            smtpClient.Host = "relay-hosting.secureserver.net";
            // smtpClient.Port = 25;

            smtpClient.Send(message);
            msg = "ok";
        }

        catch (Exception ex)
        {
            msg = ex.Message;

            //throw ex;
        }

    }
    public void Clear()
    {

        txtAAdhaar.Text = "";
        txtCountrycode.Text = "";
        txtPhoneno.Text = "";
        txtAreacode.Text = "";
        txtemailid.Text = "";
        txtResCountry.Text = "";
        txtResPincode.Text = "";
        txtResTown.Text = "";
        txtResArea.Text = "";
        txtResStreet.Text = "";

        txtResFlat.Text = "";

        txtLastName.Text = "";
        txtFirstName.Text = "";
        txtMiddleName.Text = "";
        txtPrintName.Text = "";
    }
    private string GeneratePassword()
    {
        string strPwdchar = "abcdefghijklmnopqrstuvwxyz0123456789#+@&$ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string strPwd = "";
        Random rnd = new Random();
        for (int i = 0; i <= 7; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            strPwd += strPwdchar.Substring(iRandom, 1);
        }
        return strPwd;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {


    }
    public string date(string date)
    {
        string da = "";

        string[] date1 = date.Split('/');

        da = date1[2] + "/" + date1[1] + "/" + date1[0];

        return da;
    }
}
