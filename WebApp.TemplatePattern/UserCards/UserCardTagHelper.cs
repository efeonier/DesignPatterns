using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebApp.TemplatePattern.Entities;

namespace WebApp.TemplatePattern.UserCards;

public class UserCardTagHelper : TagHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserCardTagHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public AppUser AppUser { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        UserCardTemplate userCardTemplate;
        if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            userCardTemplate = new PrimeUserCardTemplate();
        else
            userCardTemplate = new DefaultUserCardTemplate();

        userCardTemplate.SetUser(AppUser);
        output.Content.SetHtmlContent(userCardTemplate.Build());
    }
}
