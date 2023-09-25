# Welcome to the Simple Markdown CMS

This project is based, loosely, on [Nuxt Content](https://content.nuxt.com/). The idea is this: 

 - You create your content as Markdown files, adding meta data as [YAML frontmatter](https://jekyllrb.com/docs/front-matter/).
 - You can create single documents and expose them individually - a blog post, for example, or you can create "blocks", organized by directory.
 - Query using LINQ

## Blocks? What Do You Mean?

Blog posts are simple to store as a single markdown file, but when you need to structure your content for a landing page, for instance, things get more difficult. You have sections, or "blocks" as they're called in WordPress, Ghost, and other CMS platforms, which require they're own layout settings.

You _could_ store all of this in a database or you could store the information in Markdown, decorating it with frontmatter:

```md
---
title: A Simple Markdown CMS
cta: "https://test.com"
image: "https://test.com/images/hero.png"
---

When you're just getting started, sometimes Markdown is **all you need**.
```

We could call this file `hero.md` and save it in the `/home` directory. We might have other documents in there, such as `second_headline.md`, `call_to_action.md` and subdirectory called `/features`. The idea is that each of these files represents a bit of content that we need to build our page.

## Querying Content

Your frontend code would need to make a call to the content api, specifying what it wants. Let's query for the home page documents:

```js
const res = await fetch("/api/content/home");
const docs = await res.json();
```

The result will be an array containing all of the document data that you just pop into place.

## The Document

The data returned is represented by a `Document` model, which is a very basic representation of typical CMS content:

```cs
using YamlDotNet.Serialization;
namespace Tailwind.CMS.Data.Models
{
  public partial class Document
  {
    public Document(){}
    public string Directory { get; set; }
    public string Path { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public string HTML { get; set; }
    public string RawText { get; set; }
    [YamlMember(Alias = "title")]
    public string Title{ get; set; }
    [YamlMember(Alias = "summary")]
    public string Summary{ get; set; }
    [YamlMember(Alias = "index")]
    public int Index{ get; set; }
    [YamlMember(Alias = "icon")]
    public string Icon{ get; set; }
    [YamlMember(Alias = "image")]
    public string Image { get; set; }
    [YamlMember(Alias = "category")]
    public string Category { get; set; }
    [YamlMember(Alias = "tags")]
    public string[] Tags { get; set; }
    [YamlMember(Alias = "lede")]
    public string Lede { get; set; }
    [YamlMember(Alias = "cta")]
    public string CallToAction { get; set; }
    [YamlMember(Alias = "link")]
    public string Link { get; set; }
  }
}
```

Everything that is decorated with `YamlMember` will take its setting from the Markdown document's frontmatter. If you want to add something (e.g. a `SKU` setting for a product), you tweak the code, expand the partial class, or create a new class called `Product` that inherits from `Document`. Up to you.

## Deployment

I believe strongly in "Day Zero Deployment" which is a term I made up for rapidly getting your idea in front of people, on the very first day. This entire project is based on this idea, so to that end, there's a `Deployment` directory with some Azure scripts in it.

### The Simplest Thing: Zip

At some point you'll want to orchestrate this build using some type of CI/CD, such as GitHub Actions, but on day 0 you just need something up and running - that's what we've made for you.

There's also a Dockerfile if you want to deploy using that, but that will cost money and the App Service Zip deployment is **free** providing you don't have another F1 (free tier) app service plan running.

### The First Thing: Your Azure Resources

In the `/Deployment/Azure` directory you'll see `app_service.sh`, which is a shell script you can edit as needed. You should only need to check the top lines, which contain your resource group name, app name, location and app service plan (which is defaulted to free). Once you set these, you can run this script in bash or Powershell from the root of the project:

```
source ./Deployment/Azure/app_service.sh
```

Or, if you're like me, you could use a Makefile and flex:

```
make app_service
```

This script will create the resources you need and deploy your site to Azure. For future deployments, you only need to run the `zip.sh` file, which was created for you by the `app_service.sh` script:

```
source ./Deployment/Azure/zip.sh
```

Or, flex even harder with Make:

```
make
```

### Viewing the Logs

You can see what's happening with your app up on Azure by streaming the logs to your terminal:

```
source ./Deployment/Azure/logs.sh
```

Or, of course, you can just use Make:

```
make logs
```

## Why Are The Markdown Files in wwwroot?

In short, because when you build an ASP.NET application your static content from `wwwroot` is copied over completely. I'm hoping to find a simpler way to do this, but for now that's the way it is.

## Help? Questions?

Happy to look at any issues or consider a PR but please: **create an issue before a PR**. I might be working on the same thing as you!

We also have [a discussions site you can play around at](https://github.com/orgs/tailwind-traders-dev/discussions). Come say hi!