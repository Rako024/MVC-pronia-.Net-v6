using Business.Exceptions;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC_pronia.Areas.Admin.Controllers;

[Area("Admin")]
public class TagController : Controller
{
    ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    public IActionResult Index()
    {
        List<Tag> tags = _tagService.GetAllTags();
        return View(tags);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Tag tag)
    {
        _tagService.CreateTag(tag);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int  id)
    {
        _tagService.DeleteTag(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Update(int id)
    {
        Tag tag = _tagService.GetTag(x => x.Id == id);
        if (tag != null)
        {
            return View(tag);
        }
        throw new NotFoundTagException("Bele Id-li Tag Yoxdur");
    }
    [HttpPost]
    public IActionResult Update(Tag tag)
    {
        _tagService.UpdateTag(tag.Id, tag);
        return RedirectToAction(nameof(Index));
    }
}
