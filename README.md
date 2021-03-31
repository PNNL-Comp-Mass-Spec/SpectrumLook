# SpectrumLook

SpectrumLook can be used to visually annotate fragmentation spectra in an LC-MS/MS dataset
with peptide sequence matches (PSMs).

## Features

SpectrumLook reads Thermo .Raw files, or .mzXml files, or .mzML files, 
along with a "Synopsis File" listing peptides identified by a MS/MS search
tool like MS-GF+, SEQUEST, or X!Tandem. Synopsis files are created by the
Peptide Hit Results Processor, [Available on GitHub](https://github.com/PNNL-Comp-Mass-Spec/PHRP/releases/)

SpectrumLook allows users to visually browse the MS/MS spectra 
that led to the peptide identifications, including viewing annotations 
for the identified b and y ions for CID or HCD spectra (or c and z ions for ETD spectra).
It also shows neutral loss ions where appropriate. 

In order to properly annotate dynamic and static modifications on residues,
the ModSummary.txt file must be in the same directory as the synopsis file.
For reference, see these two files:
* [QC_Mam_19_01_excerpt_msgfplus_syn.txt](https://github.com/PNNL-Comp-Mass-Spec/SpectrumLook/blob/master/TestData/MSGFPlus/QC_Mam_19_01_excerpt_msgfplus_syn.txt)
* [QC_Mam_19_01_excerpt_msgfplus_syn_ModSummary.txt](https://github.com/PNNL-Comp-Mass-Spec/SpectrumLook/blob/master/TestData/MSGFPlus/QC_Mam_19_01_excerpt_msgfplus_syn_ModSummary.txt)

Proteowizard's MSConvert tool can be used to convert most vendor formats 
into either mzML or mzXML files; download from:
* http://proteowizard.sourceforge.net/

## Alternatives

For visualizing MS-GF+ results (.mzid files), consider using LCMS Spectator. \
Download from [PNNL-Comp-Mass-Spec/LCMS-Spectator/releases](https://github.com/PNNL-Comp-Mass-Spec/LCMS-Spectator/releases) on GitHub

## Contacts

Written in 2011 by a Washington State University senior design team, overseen by Brian LaMarche for the Department of Energy (PNNL, Richland, WA) \
Additional contributions from Bryson Gibbons and Matthew Monroe \
Copyright 2021, Battelle Memorial Institute.  All Rights Reserved. \
E-mail: proteomics@pnnl.gov \
Website: https://panomics.pnl.gov/ or https://omics.pnl.gov

## License

Licensed under the Apache License, Version 2.0; you may not use this program except
in compliance with the License.  You may obtain a copy of the License at
http://www.apache.org/licenses/LICENSE-2.0

RawFileReader reading tool. Copyright © 2016 by Thermo Fisher Scientific, Inc. All rights reserved.
