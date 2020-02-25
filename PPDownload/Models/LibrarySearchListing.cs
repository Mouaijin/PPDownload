using System;

namespace PPDownload.Models
{
    /// <summary>
    /// Model for a single listing from a score library search
    /// </summary>
    public class LibrarySearchListing
    {
        /// <summary>
        /// Title of the score
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Creator of the score
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Description of the score
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Upload date for the score
        /// </summary>
        public DateTime Uploaded { get; set; }

        /// <summary>
        /// Link to the target video for the score
        /// </summary>
        public string VideoLink { get; set; }

        /// <summary>
        /// Link to initiate download of zipped score
        /// </summary>
        public string DownloadLink { get; set; }

        /// <summary>
        /// Easy-mode difficulty, if score included
        /// </summary>
        public decimal? Easy { get; set; }

        /// <summary>
        /// Normal-mode difficulty, if score included
        /// </summary>
        public decimal? Normal { get; set; }

        /// <summary>
        /// Hard-mode difficulty, if score included
        /// </summary>
        public decimal? Hard { get; set; }

        /// <summary>
        /// Extreme-mode difficulty, if score included
        /// </summary>
        public decimal? Extreme { get; set; }
        
        /// <summary>
        /// Number of times score has been downloaded
        /// </summary>
        public int? Downloads { get; set; }
        
        /// <summary>
        /// Number of times score has been downloaded
        /// </summary>
        public int? BPM { get; set; }
        
        /// <summary>
        /// Rating for the score
        /// </summary>
        public decimal? Evaluation { get; set; }
        
        /// <summary>
        /// Number of evaluations
        /// </summary>
        public int? Evaluators { get; set; }
        
        /// <summary>
        /// Score duration, as displayed on the PPD site
        /// </summary>
        public string Length { get; set; }
    }
}