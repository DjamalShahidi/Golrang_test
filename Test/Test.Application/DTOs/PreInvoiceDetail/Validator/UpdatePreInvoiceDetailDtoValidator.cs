﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Application.Contracts.Persistence;
using Test.Application.DTOs.PreInvoiceHeader;
using Test.Domain;

namespace Test.Application.DTOs.PreInvoiceDetail.Validator
{
    public class UpdatePreInvoiceDetailDtoValidator : AbstractValidator<UpdatePreInvoiceDetailDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePreInvoiceDetailDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            Domain.PreInvoiceHeader preInvoiceHeader = null;


            RuleFor(a => a.Id)
                .GreaterThan(0).WithMessage("Invalid Id")
                .MustAsync(async (id, token) =>
                {
                    var preInvoiceDetail = await _unitOfWork.PreInvoiceDetailRepository.GetAsync(id);

                    if (preInvoiceDetail == null)
                    {
                        return false;
                    }
                    preInvoiceHeader = await _unitOfWork.PreInvoiceHeaderRepository.GetAsync(preInvoiceDetail.PreInvoiceHeaderId);

                    if (preInvoiceHeader == null)
                    {
                        return false;
                    }
                    else if (preInvoiceHeader.Status == PreInvoiceHeaderStatus.Final)
                    {
                        return false;
                    }
                    return true;

                }).WithMessage("NotExist PreInvoiceDeail or PreInvoiceHeader status is Final");



            RuleFor(a => a.ProductId)
             .GreaterThan(0).WithMessage("Invalid ProductId")
             .MustAsync(async (productId, token) =>
             {
                 if (preInvoiceHeader != null)
                 {
                     var isExist = await _unitOfWork.ProductSaleLineRepository.IsExist(productId, preInvoiceHeader.SalesLineId);
                     return isExist;
                 }
                 else
                 {
                     return false;
                 }
             }).WithMessage("Not Exist product in this line")
             .MustAsync(async (model ,productId,token) =>
             {
                 if (preInvoiceHeader != null)
                 {
                     var isExist = await _unitOfWork.PreInvoiceDetailRepository.CheckDuplicateProductForUpdate(preInvoiceHeader.Id, model.Id,productId);
                     if (isExist)
                     {
                         return false;
                     }
                     return true;
                 }
                 else
                 {
                     return false;
                 }
             }).WithMessage("Duplicate product");

            RuleFor(a => a.Price)
              .GreaterThan(0).WithMessage("Invalid Price");

            RuleFor(a => a.Count)
              .GreaterThan(0).WithMessage("Invalid Count");

        }
    }
}
