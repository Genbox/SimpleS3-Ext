﻿using Genbox.SimpleS3.Core.Abstracts;
using Genbox.SimpleS3.Core.Network.Requests.Buckets;
using Genbox.SimpleS3.Core.Network.Responses.Buckets;
using Genbox.SimpleS3.Utility.Shared;

namespace Genbox.ProviderTests.Misc;

public abstract class TestBase
{
    protected string GetTemporaryBucket() => UtilityHelper.GetTemporaryBucket();

    protected async Task CreateTempBucketAsync(S3Provider provider, ISimpleClient client, Func<string, Task> action, Action<CreateBucketRequest>? config = null)
    {
        string tempBucket = GetTemporaryBucket();

        CreateBucketResponse createResponse = await client.CreateBucketAsync(tempBucket, config).ConfigureAwait(false);
        Assert.Equal(200, createResponse.StatusCode);

        try
        {
            await action(tempBucket).ConfigureAwait(false);
        }
        finally
        {
            int errors = await UtilityHelper.ForceEmptyBucketAsync(provider, client, tempBucket);

            if (errors == 0)
            {
                DeleteBucketResponse delBucketResp = await client.DeleteBucketAsync(tempBucket).ConfigureAwait(false);
                Assert.True(delBucketResp.IsSuccess);
            }

            Assert.Equal(0, errors);
        }
    }
}