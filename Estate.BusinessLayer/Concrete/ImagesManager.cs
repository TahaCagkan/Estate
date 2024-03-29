﻿using Estate.BusinessLayer.Abstract;
using Estate.DataAccessLayer.Abstract;
using Estate.EntityLayer.Entities;
using System.Linq.Expressions;

namespace Estate.BusinessLayer.Concrete
{
    public class ImagesManager : IImagesService
    {
        IImagesRepository _imagesRepository;

        public ImagesManager(IImagesRepository imagesRepository)
        {
            _imagesRepository = imagesRepository;
        }
        public void Add(Images parameter)
        {
            _imagesRepository.Add(parameter);
        }

        public void Delete(Images parameter)
        {
            _imagesRepository.Delete(parameter);
        }

        public List<Images> GetAllList()
        {
            return _imagesRepository.GetAllList();
        }

        public Images GetById(int id)
        {
            return _imagesRepository.GetById(id);
        }

        public List<Images> GetList(Expression<Func<Images, bool>> filter)
        {
            return _imagesRepository.GetList(filter);
        }

        public void Update(Images parameter)
        {
            var update = _imagesRepository.GetById(parameter.ImageId);
            update.ImageName = parameter.Image.FileName;
            _imagesRepository.Update(update);
        }
    }
}

