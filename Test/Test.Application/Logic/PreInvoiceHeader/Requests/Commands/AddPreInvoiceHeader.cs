﻿using MediatR;
using Store.Application.Responses;
using Test.Application.DTOs.PreInvoiceHeader;

namespace Test.Application.Logic.PreInvoiceHeader.Requests.Commands
{
    public class AddPreInvoiceHeader : IRequest<Response>
    {
        public AddPreInvoiceHeaderDto Request { get; set; }
    }
}
