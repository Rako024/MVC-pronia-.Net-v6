using Business.Exceptions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class TagService : ITagService
    {
        ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public void CreateTag(Tag tag)
        {
            _tagRepository.Add(tag);
            _tagRepository.Commit();
        }

        public void DeleteTag(int id)
        {
            Tag existTag = _tagRepository.Get(x=>x.Id == id);
            if (existTag != null)
            {
                _tagRepository.Delete(existTag);
                _tagRepository.Commit();
                return;
            }
            throw new NotFoundTagException("Bele bir Tag Yoxdur!!!");
        }

        public List<Tag> GetAllTags(Func<Tag, bool>? func = null)
        {
            return _tagRepository.GetAll(func);
        }

        public Tag GetTag(Func<Tag, bool>? func = null)
        {
            return _tagRepository.Get(func);
        }

        public void UpdateTag(int id, Tag tag)
        {
            Tag oldTag = _tagRepository.Get(x => x.Id == id);
            if (oldTag != null)
            {
                oldTag.Name = tag.Name;
                _tagRepository.Commit();
                return;
            }
            throw new NotFoundTagException("Bele bir Tag Yoxdur!!!");
        }
    }
}
