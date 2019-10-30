﻿using FluentValidation;
using Genbox.SimpleS3.Core.Network.Requests.Buckets;
using Microsoft.Extensions.Options;

namespace Genbox.SimpleS3.Core.Validation.Validators.Requests.Buckets
{
    public class ListMultipartUploadsRequestValidator : BaseRequestValidator<ListMultipartUploadsRequest>
    {
        public ListMultipartUploadsRequestValidator(IOptions<S3Config> config) : base(config)
        {
            RuleFor(x => x.ObjectKey).Empty();
            RuleFor(x => x.MaxUploads).GreaterThan(0).LessThanOrEqualTo(1000).When(x => x.MaxUploads != null);
        }
    }
}