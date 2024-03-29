﻿using AutoMapper;
using MediatR;
using Store.Application.Responses;
using Test.Application.Contracts.Persistence;
using Test.Application.DTOs.PreInvoiceDetail.Validator;
using Test.Application.Logic.PreInvoiceDetail.Requests.Commands;
using Test.Domain;

namespace Test.Application.Logic.PreInvoiceDetail.Handlers.Commands
{
    public class DeletePreInvoiceDetailHandler : IRequestHandler<DeletePreInvoiceDetail, Response>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePreInvoiceDetailHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeletePreInvoiceDetail request, CancellationToken cancellationToken)
        {
            try
            {

                var validator = new DeletePreInvoiceDetailDtoValidator(_unitOfWork);

                var validatorResult = await validator.ValidateAsync(request.Request);

                if (validatorResult.IsValid == false)
                {
                    return new Response()
                    {
                        ErrorMessages = validatorResult.Errors.Select(a => a.ErrorMessage).ToList(),
                        IsSuccess = false,
                        Result = null
                    };
                }


                var preInvoiceDetail = await _unitOfWork.PreInvoiceDetailRepository.GetAsync(request.Request.Id);

                _unitOfWork.PreInvoiceDetailRepository.Delete(preInvoiceDetail);

                await _unitOfWork.Save(cancellationToken);

                return new Response()
                {
                    IsSuccess = true,
                    Result = preInvoiceDetail.Id
                };

            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsSuccess = false,
                    ErrorMessages = [ex.Message]
                };
            }
        }

    }
}
