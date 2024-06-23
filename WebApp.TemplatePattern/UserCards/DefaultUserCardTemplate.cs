namespace WebApp.TemplatePattern.UserCards;

public class DefaultUserCardTemplate : UserCardTemplate
{
    protected override string SetFooter()
    {
        return string.Empty;
    }

    protected override string SetPicture()
    {
        return $"<img src='/userPictures/defaultuserpicture.png' class='card-img-top' alt=''>";
    }
}