using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Storage;
using NLog;
using TheGramPost.Helpers;

namespace TheGramPost
{
    public class FirebaseService : IFirebaseService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

//        private const string ApiKey = "AIzaSyBgAq89XA31cyg_o9LMAD5YaxEC0K3re9M";
        private const string Bucket = "the-gram-c0daa.appspot.com";
        private readonly IUserContextHelper _userContextHelper;


        public FirebaseService(IUserContextHelper userContextHelper)
        {
            this._userContextHelper = userContextHelper;
        }

        public async Task<string> UploadFile(FileStream file, DateTime timePosted)
        {
            var fullFileName = file.Name.Split('/').Last().Split('.');
            var fileName = fullFileName[0];
            var extension = fullFileName.Last();
            fileName = $"{fileName}-{timePosted.ToBinary()}.{extension}";
            var userID = _userContextHelper.GetUserId();
             
            // Cancellation Token
            var cancellation = new CancellationTokenSource();
            var upload = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = _userContextHelper.GetAuthToken,
                        ThrowOnCancel = true
                    })
                .Child($"posts/{userID}")
                .Child($"{fileName}")
                .PutAsync(file, cancellation.Token);

            try
            {
                return await upload;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }
    }
}