namespace monke
{
    // Basically enums
    public record KeyboardModel(string DisplayName, string Path)
    {
        public static readonly KeyboardModel[] models = {
            new KeyboardModel("Alpaca", "alpaca"),
            new KeyboardModel("Black Ink", "blackink"),
            new KeyboardModel("Blue Alps", "bluealps"),
            new KeyboardModel("Box Navy", "boxnavy"),
            new KeyboardModel("Buckling", "buckling"),
            new KeyboardModel("Cream", "cream"),
            new KeyboardModel("Holy Panda", "holypanda"),
            new KeyboardModel("MX Black", "mxblack"),
            new KeyboardModel("MX Blue", "mxblue"),
            new KeyboardModel("MX Brown", "mxbrown"),
            new KeyboardModel("Red Ink", "redink"),
            new KeyboardModel("Topre", "topre"),
            new KeyboardModel("Turqoise", "turqoise")
        };
    }
}
