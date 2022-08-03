# Welcome

Hi! Welcome to [Planet .NET - Preview](https://brave-field-0981c0d03.1.azurestaticapps.net/)!

If you write about .NET, you belong here. You're welcome to add your blog and have it aggregated as part of our feed as long as the content you are sharing does not violate the community [code of conduct](https://github.com/mabroukmahdhi/planetdotnet/blob/master/CODE_OF_CONDUCT.md).

Many thanks to the [contributors of PlanetXamarin](https://github.com/planetxamarin/planetxamarin/graphs/contributors) for the template!

# Add yourself as an author

### Author Guidelines
- I have a valid blog & RSS URL, both using HTTPS with a valid certificate
- Host NO malicious or offensive content on the blog (including photos, swearing, etc.)
- Blog is active with at least 3 .NET related blog posts in the last 6 months
- If the blog has mixed content (.NET and Personal/Non-.NET blogs) a filter has been applied
- If you delete your blog you will come delete your blog from Planet .NET
- Your blog may be removed at any time if any of these are broken.

### How to add

To add yourself as an author you can fork this project, add yourself to the [authors folder](https://github.com/mabroukmahdhi/planetdotnet/tree/main/PlanetDotnet.Api/Models/Foundations/Authors) as a class, implementing the `IAmACommunityMember` interface. If you are doing this via the GitHub editor, don't forget to _add the class to the .csproj_.

The result should look something like this:

``` csharp
public class BruceWayne : IAmACommunityMember
{
    public string FirstName => "Bruce";
    public string LastName => "Wayne";
    public string ShortBioOrTagLine => "potentially batman";
    public string StateOrRegion => "Gotham";
    public string EmailAddress => "some@email.com";
    public string TwitterHandle => "theplanetdotnet";
    public string GravatarHash => "42abc1337def";
    public string GitHubHandle => "some_github_id";
    public GeoPosition Position => new GeoPosition(47.643417, -122.126083);
    public Uri WebSite => new Uri("https://brave-field-0981c0d03.1.azurestaticapps.net/");
    public IEnumerable<Uri> FeedUris { get { yield return new Uri("https://brave-field-0981c0d03.1.azurestaticapps.net/api/rss"); } }
    public string FeedLanguageCode => "en";
    
    // This property should be like this. (Intern use)
    public string Avatar { get; set; }
}
```

A few things: 
- Name the class after your first and lastname with PascalCase
- The `FirstName` and `LastName` property should resemble that same name
- `ShortBioOrTagLine` property can be whatever you like. If you can't think of anything choose: 'software engineer' or 'software engineer at Microsoft'. Please keep it short, like a 140 character tweet.
- `StateOrRegion` will be your geographical location, i.e.: Holland, New York, etc.
- `EmailAddress`, `TwitterHandle` and `GitHubHandle` should be pretty clear, `TwitterHandle` without the leading @
- `Position` is your latitude and longitude, this allows you to be placed on the map on the Authors page
- The `Website` property can be your global website or whatever you want people to look at
- With `FeedUris` you can supply one or more URIs which resemble your blogs. Your blogs should be provided in RSS (Atom) format and of course be about Xamarin.
- And finally `FeedLanguageCode` specifies in what lanuage the majority of your content will be. This is used to be able to apply filters to the feed. This language code should be in [ISO 639-1 format](https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes)
- If you do not want your e-mailaddress publicly available but you _do_ want to show your Gravatar go to https://en.gravatar.com/site/check/ and get your hash! If you don't fill the hash, you will be viewed as a silhouette.

If you also do some blogging about other stuff, no worries! You're fine! We will read only the feeds that contains one of [the categories](https://github.com/mabroukmahdhi/planetdotnet/blob/main/PlanetDotnet.Shared/Abstractions/Tags/Tag.cs) defined in the class _Tag.cs_.


# A small step for an author...

A big step for mankind! Last thing that remains is submit a Pull Request to us and whenever it gets merged: hooray! You're an author now!

Don't forget to incorporate the Featured on Planet .NET badge on your blog and link back to us!

![Planet .NET Author Badge](https://github.com/mabroukmahdhi/planetdotnet/blob/main/Assets/Badge/Badge0.png)

Enjoy all of our great content! 

Of course you are more than welcome to submit other features and bugfixes as well.

# Build Status
[![Github Actions](https://github.com/mabroukmahdhi/planetdotnet/actions/workflows/azure-static-web-apps-brave-field-0981c0d03.yml/badge.svg)](https://github.com/mabroukmahdhi/planetdotnet/actions/workflows/azure-static-web-apps-brave-field-0981c0d03.yml)

# Acknowledgements
* Thanks again for the [contributors of PlanetXamarin](https://github.com/planetxamarin/planetxamarin/graphs/contributors) for the greate template.
* Thanks to [our awesome contributors](https://github.com/mabroukmahdhi/planetdotnet/graphs/contributors) and our [community of authors](https://github.com/mabroukmahdhi/planetdotnet/tree/main/PlanetDotnet.Api/Models/Foundations/Authors) who make this all possible.
