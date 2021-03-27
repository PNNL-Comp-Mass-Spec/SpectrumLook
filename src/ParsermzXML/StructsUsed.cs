
// TODO: Structs Used is a horrible name for this file.  Pleaes rename it.
// TODO: break up the structures into their own files.

namespace SpectrumLook
{

    // TODO: Add Comments to this
    public struct ComparedElement
    {
        // TODO: Add Comments to this
        public double MZValue;
        // TODO: Add Comments to this
        public string Annotation;

        // TODO: Add Comments to this
        // TODO: lower case breaks coding standard.
        public bool matched;
        // TODO: Add Comments to this
        public double Intensity;
    }

    // TODO: Add Comments to this
    public struct ActualElement
    {
        // TODO: Add Comments to this
        public double MZValue;
        // TODO: Add Comments to this
        public double Intensity;
    }

    // TODO: Add Comments to this
    public struct TheoryElement
    {
        // TODO: Add Comments to this
        public double theoryMZ;
        // TODO: Add Comments to this
        public string theoryAnnotation;
    }
}