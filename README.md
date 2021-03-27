# SpectrumLook

SpectrumLook can be used to visually inspect peptide sequence matches (PSMs) 
against fragmentation spectra in an LC-MS/MS dataset.

## Alternatives

For visualizing MS-GF+ results (.mzid files), consider using LCMS Spectator. \
Download from [PNNL-Comp-Mass-Spec/LCMS-Spectator/releases](https://github.com/PNNL-Comp-Mass-Spec/LCMS-Spectator/releases) on GitHub

## Features

SpectrumLook reads Thermo .Raw files or .mzXml files, plus a 
"Synopsis File" listing peptides identified by a MS/MS interpretation 
tool like MS-GF+, SEQUEST, or X!Tandem. The software allows users to 
visually browse the MS/MS spectra that led to the peptide 
identifications, including viewing annotations for the identified b 
and y ions, plus neutral loss ions where appropriate. See the Peptide 
Hit Results Processor page for more information on Synopsis and First 
hits files.

SpectrumLook supports CID, ETD, and HCD spectra.  It also supports 
dynamic and static modifications on residues.

SpectrumLook also supports the mzXML format. You can use 
Proteowizard's MSConvert tool to convert most vendor formats into the 
mzXML format.


## Contacts

Written by a Washington State University senior design team, overseen by Brian LaMarche for the Department of Energy (PNNL, Richland, WA) \
Additional contributions from Bryson Gibbons and Matthew Monroe \
Copyright 2017, Battelle Memorial Institute.  All Rights Reserved. \
E-mail: proteomics@pnnl.gov \
Website: https://panomics.pnl.gov/ or https://omics.pnl.gov

## License

Licensed under the Apache License, Version 2.0; you may not use this program except
in compliance with the License.  You may obtain a copy of the License at
http://www.apache.org/licenses/LICENSE-2.0

RawFileReader reading tool. Copyright © 2016 by Thermo Fisher Scientific, Inc. All rights reserved.
