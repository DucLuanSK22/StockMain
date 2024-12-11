namespace Stock.BE.Email
{
    public interface IEmail
    {
        void SendMail(EmailDto emailDto);
    }
}
