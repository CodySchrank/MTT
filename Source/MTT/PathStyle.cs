namespace MTT
{
    public enum PathStyle
    {
        /// <summary>
        /// Directories are named as they're named in the source. File names are converted to camelCase.
        /// </summary>
        Default,

        /// <summary>
        /// Directory and file names are converted to kebab-case.
        /// </summary>
        Kebab
    }
}
