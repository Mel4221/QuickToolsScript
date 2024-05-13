
namespace Parser
{
    public partial class CodeParser
    {

        /// <summary>
        /// Gets or sets the token for the current code parsing and execution
        /// </summary>
        /// <value>The token.</value>
        public int Token { get; set; } = QuickTools.QCore.IRandom.RandomInt(10000, 99999);
        /// <summary>
        /// Gets or sets the current shell path.
        /// </summary>
        /// <value>The current shell path.</value>
        public string CurrentShellPath { get; set; } = "NOT-DEFINED";
        /// <summary>
        /// Gets or sets the code from the CodeParser
        /// </summary>
        /// <value>The code.</value>
        public string[] Code            { get; set; } = new string[0];
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        public string Action            { get; set; } = "NULL";
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type              { get; set; } = "NULL";
        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public string[] Parameters      { get; set; } = new string[0];
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Parser.CodeParser"/> has a loop opened.
        /// </summary>
        /// <value><c>true</c> if is loop opened; otherwise, <c>false</c>.</value>
        public bool IsLoopOpened        { get; set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Parser.CodeParser"/> has a condition opened.
        /// </summary>
        /// <value><c>true</c> if is condition opened; otherwise, <c>false</c>.</value>
        public bool IsConditionOpened   { get; set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Parser.CodeParser"/> has a function opened.
        /// </summary>
        /// <value><c>true</c> if is function opened; otherwise, <c>false</c>.</value>
        public bool IsFunctionOpened    { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Parser.CodeParser"/> has error flags.
        /// </summary>
        /// <value><c>true</c> if has error flags; otherwise, <c>false</c>.</value>
        public bool HasErrorFlags { get; set; } = false;
    }
}
