﻿using MediatR;
using Store.Application.Responses;
using Test.Application.DTOs.PreInvoiceDetail;

namespace Test.Application.Logic.PreInvoiceHeader.Requests.Commands
{
    public class AddPreInvoiceDetail : IRequest<Response>
    {
        public AddPreInvoiceDetailDto Request { get; set; }
    }
}
