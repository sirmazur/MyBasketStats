﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MyBasketStats.API.Entities;
using MyBasketStats.API.Models;
using MyBasketStats.API.Services.PlayerServices;
using MyBasketStats.API.Services.TeamServices;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace MyBasketStats.API.Services.Basic
{
    public class BasicService<TDto, TEntity, TExtendedDto> where TEntity : class where TDto : class where TExtendedDto : class
    {
        protected readonly IMapper _mapper;
        protected readonly IBasicRepository<TEntity> _basicRepository;
        public BasicService(IMapper mapper, IBasicRepository<TEntity> basicRepository)
        {
            _mapper=mapper;
            _basicRepository=basicRepository;
        }

        public async Task<TDto> GetByIdAsync(int id)
        {
            var item = await _basicRepository.GetByIdAsync(id);
            var itemToReturn = _mapper.Map<TDto>(item);
            return itemToReturn;
        }

        public async Task<TEntity> GetEntityByIdAsync(int id)
        {
            var item = await _basicRepository.GetByIdAsync(id);
            return item;
        }

        public async Task<TDto> GetByIdWithEagerLoadingAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var item = await _basicRepository.GetByIdWithEagerLoadingAsync(id, includeProperties);
            var itemToReturn = _mapper.Map<TDto>(item);
            return itemToReturn;
        }

        public async Task<TExtendedDto> GetExtendedByIdWithEagerLoadingAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var item = await _basicRepository.GetByIdWithEagerLoadingAsync(id, includeProperties);
            var itemToReturn = _mapper.Map<TExtendedDto>(item);
            return itemToReturn;
        }

        public async Task<TEntity> GetEntityByIdWithEagerLoadingAsync(int id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return await _basicRepository.GetByIdWithEagerLoadingAsync(id, includeProperties);
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var listToReturn = await _basicRepository.GetAllAsync();
            var finalListToReturn = _mapper.Map<IEnumerable<TDto>>(listToReturn);
            return finalListToReturn;
        }

        public async Task<IEnumerable<TExtendedDto>> GetExtendedListWithEagerLoadingAsync(IEnumerable<int> ids, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            List<TExtendedDto> list = new List<TExtendedDto>();
            foreach (var id in ids) 
            {
                var add = await _basicRepository.GetByIdWithEagerLoadingAsync(id, includeProperties);
                list.Add(_mapper.Map<TExtendedDto>(add));
            }
            return list;
        }

        public async Task<(bool,TEntity?)> CheckIfIdExistsAsync(int id)
        {
            return await _basicRepository.CheckIfIdExistsAsync(id);
        }
        public virtual async Task<OperationResult<TDto>> DeleteByIdAsync(int id)
        {
            (bool exists, TEntity ? entity) result = await _basicRepository.CheckIfIdExistsAsync(id);
            if (result.exists == false)
            {
                return new OperationResult<TDto>
                {
                    IsSuccess = false,
                    ErrorMessage = $"Entity of type {typeof(TEntity).Name} with id={id} does not exist.",
                    HttpResponseCode = 404
                };
            }
            else
            {
                _basicRepository.DeleteAsync(result.entity);
                await _basicRepository.SaveChangesAsync();
                return new OperationResult<TDto>
                {
                    IsSuccess = true,
                    HttpResponseCode = 204
                };
            }
        }

    }
}
