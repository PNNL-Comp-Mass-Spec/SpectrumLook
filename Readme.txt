SpectrumLook can be used to inspect the fragmentation (MS/MS) 
spectra in an LC-MS/MS analysis.

SpectrumLook reads Finnigan .Raw files or .mzXml files, plus a "Synopsis File" listing peptides identified by a MS/MS interpretation tool like MSGF+, SEQUEST, or X!Tandem. The software allows users to visually browse the MS/MS spectra that led to the peptide identifications, including viewing annotations for the identified b and y ions, plus neutral loss ions where appropriate. See the Peptide Hit Results Processor page for more information on Synopsis and First hits files.

SpectrumLook supports CID, ETD, and HCD spectra.  It also supports dynamic and static modifications on residues.

SpectrumLook can directly read Finnigan .RAW files, provided that XRawFile2.dll v2.2 is installed on the computer. This file is installed with Xcalibur 2.2, but can also be installed using MSFileReader v2.2, available on Thermo's MSFileReader page

SpectrumLook also supports the mzXML format. You can use Proteowizard's MSConvert tool to convert most vendor formats into the mzXML format.
