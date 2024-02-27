﻿namespace Store.Application.Responses
{
    public class Response
    {
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }=new List<string>();
        public object Result { get; set; } = null;
    }
}
