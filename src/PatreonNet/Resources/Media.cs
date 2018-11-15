using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PatreonNet.Resources
{
    /// <summary>
    /// A file uploaded to patreon.com, usually an image.
    /// </summary>
    public class Media : PatreonObject
    {
        /// <summary>
        /// File name.
        /// </summary>
        [JsonProperty(PropertyName = "file_name")]
        public string FileName { get; set; }

        /// <summary>
        /// Size of file in bytes.
        /// </summary>
        [JsonProperty(PropertyName = "size_bytes")]
        public int SizeBytes { get; set; }

        /// <summary>
        /// Mimetype of uploaded file.
        /// </summary>
        /// <example>application/jpeg</example>
        [JsonProperty(PropertyName = "mimetype")]
        public string MimeType { get; set; }

        /// <summary>
        /// Upload availability state of the file.
        /// </summary>
        [JsonProperty(PropertyName = "state")]
        public string state { get; set; }

        /// <summary>
        /// Type of the resource that owns the file.
        /// </summary>
        [JsonProperty(PropertyName = "owner_type")]
        public string OwnerType { get; set; }

        /// <summary>
        /// Ownership id(See also owner_type).
        /// </summary>
        [JsonProperty(PropertyName = "owner_id")]
        public string OwnerId { get; set; }

        /// <summary>
        /// Ownership relationship type for multi-relationship medias.
        /// </summary>
        [JsonProperty(PropertyName = "owner_relationship")]
        public string OwnerRelationship { get; set; }

        /// <summary>
        /// When the upload URL expires.
        /// </summary>
        [JsonProperty(PropertyName = "upload_expires_at")]
        public DateTimeOffset upload_expires_at { get; set; }

        /// <summary>
        /// The URL to perform a POST request to in order to upload the media file.
        /// </summary>
        [JsonProperty(PropertyName = "upload_url")]
        public string UploadUrl { get; set; }

        /// <summary>
        /// All the parameters that have to be added to the upload form request.
        /// </summary>
        [JsonProperty(PropertyName = "upload_parameters")]
        public string UploadParameters { get; set; }

        /// <summary>
        /// The URL to download this media.Valid for 24 hours.
        /// </summary>
        [JsonProperty(PropertyName = "download_url")]
        public string DownloadUrl { get; set; }

        /// <summary>
        /// When the file was created.
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Metadata related to the file.Can be null.
        /// </summary>
        [JsonProperty(PropertyName = "metadata")]
        public string Metadata { get; set; }
    }
}
