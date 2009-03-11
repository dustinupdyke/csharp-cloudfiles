///
/// See COPYING file for licensing information
///

using System;
using com.mosso.cloudfiles.exceptions;

namespace com.mosso.cloudfiles.domain.request
{
    /// <summary>
    /// GetStorageItemInformation
    /// </summary>
    public class GetStorageItemInformation : BaseRequest
    {
        /// <summary>
        /// GetStorageItemInformation constructor
        /// </summary>
        /// <param name="storageUrl">the customer unique url to interact with cloudfiles</param>
        /// <param name="containerName">the name of the container where the storage item is located</param>
        /// <param name="storageItemName">the name of the storage item to add meta information too</param>
        /// <param name="storageToken">the customer unique token obtained after valid authentication necessary for all cloudfiles ReST interaction</param>
        /// <exception cref="ArgumentNullException">Thrown when any of the reference parameters are null</exception>
        /// <exception cref="ContainerNameException">Thrown when the container name is invalid</exception>
        /// <exception cref="StorageItemNameException">Thrown when the object name is invalid</exception>
        public GetStorageItemInformation(string storageUrl, string storageToken, string containerName, string storageItemName)
        {
            if (string.IsNullOrEmpty(storageUrl)
                || string.IsNullOrEmpty(storageToken)
                || string.IsNullOrEmpty(containerName)
                || string.IsNullOrEmpty(storageItemName))
                throw new ArgumentNullException();


            if (!ContainerNameValidator.Validate(containerName)) throw new ContainerNameException();
            if (!ObjectNameValidator.Validate(storageItemName)) throw new StorageItemNameException();


            Uri = new Uri(storageUrl + "/" + containerName.Encode() + "/" + storageItemName.Encode());
            Method = "HEAD";
            AddStorageOrAuthTokenToHeaders(Constants.X_STORAGE_TOKEN, storageToken);
        }
    }
}