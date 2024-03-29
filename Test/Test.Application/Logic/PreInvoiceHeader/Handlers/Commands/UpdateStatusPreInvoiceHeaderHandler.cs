﻿using AutoMapper;
using MediatR;
using Store.Application.Responses;
using Test.Application.Contracts.Persistence;
using Test.Application.DTOs.PreInvoiceHeader.Validator;
using Test.Application.Logic.PreInvoiceHeader.Requests.Commands;

namespace Test.Application.Logic.PreInvoiceHeader.Handlers.Commands
{
    public class UpdateStatusPreInvoiceHeaderHandler : IRequestHandler<UpdateStatusPreInvoiceHeader, Response>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateStatusPreInvoiceHeaderHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response> Handle(UpdateStatusPreInvoiceHeader request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new UpdatePreInvoiceHeaderStatusDtoValidator(_unitOfWork);

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

                var header = await _unitOfWork.PreInvoiceHeaderRepository.GetAsync(request.Request.PreInvoiceHeaderId);

                header.Status = Domain.PreInvoiceHeaderStatus.Final;

                _unitOfWork.PreInvoiceHeaderRepository.Update(header);

                await _unitOfWork.Save(cancellationToken);

                return new Response()
                {
                    IsSuccess = true
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
