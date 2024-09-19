---

title: Isa assay file
bg-color: mint-lighter-40
border-color: mint-lighter-40
emphasis-color: white
image: ../../images/Learn-more/ARC/assayfile.svg
layout: text-bottom-image-top
index: 6

---

Assay metadata must be annotated in the file isa.assay.xlsx at the root of the assay's subdirectory.  This workbook must contain a single assay organized in one or many worksheets.
A worksheet named “assay” must store the [STUDY ASSAYS](https://github.com/nfdi4plants/ARC-specfication/blob/main/ARC%20specification.md#assay-data-and-metadata "Study Assay") section of the ISA model and is not required in the isa.investigation.xlsx. Additional worksheets must contain a table object organized on a per-row basis with the first row as column headers. 
Table objects must contain at least one source. Source sample relations must follow a unique path in a directed acyclic graph. Sources must be indicated by the column header Source Name, Samples accordingly by the header Sample Name.

