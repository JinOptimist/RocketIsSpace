namespace SpaceWeb.Service
{
    public interface ISmsService
    {
        string ConvertToDefaultPhoneNumber(string phone);
        int CreateCodeFromSms();
        void SendSMS(string tel, string text);
    }
}