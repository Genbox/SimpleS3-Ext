﻿using Genbox.SimpleS3.Core.Network.Requests.Objects;
using Microsoft.Extensions.Options;

namespace Genbox.SimpleS3.Core.Validation.Validators.Requests.Objects
{
    public class HeadObjectRequestValidator : RequestWithObjectKeyBase<HeadObjectRequest>
    {
        public HeadObjectRequestValidator(IOptions<S3Config> config) : base(config)
        {
        }
    }
}