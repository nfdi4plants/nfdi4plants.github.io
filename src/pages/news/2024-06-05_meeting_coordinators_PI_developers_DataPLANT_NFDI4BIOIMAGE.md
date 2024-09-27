---
date: 2024-06-05
title: Meeting of Coordinators and PIs with Developers in DataPLANT and NFDI4BIOIMAGE Projects
previewText: In the beginning of June we had a lively discussion on the current status of development in Virtual Infrastructure ontop of OpenStack and options for tighter collaborations between NFDI consortia following the Management Circle meeting of the NFDI in Freiburg. The exchange focused on... 
---
In the beginning of June we had a lively discussion on the current status of development in Virtual Infrastructure ontop of OpenStack and options for tighter collaborations between NFDI consortia following the Management Circle meeting of the NFDI in Freiburg. The exchange focused on the status of Open Source VDI development in Task Area 6 lead by Freiburg and how to proceed in the near future. The actual development in the VDI is primarily advanced in four aspects:
1. Development of a Common Access Gateway: Facilitating unified access to all VDI resources in the cloud, including a web page frontend to use prepared desktop environments customized to user requirements.
2. Construction of a Service for VM Management: Allowing service stewards or administrators to create, upload, and share new VM templates that match unique workflows.
3. Implementation of Efficient Frame Buffer Grabbing Mechanisms: Running on the host with hardware-accelerated video encoding for the remote transport protocol SPICE. SPICE enables features like bidirectional audio, printer support, and USB forwarding.
4. Integration of Hardware-Accelerated Rendering: Encompassing 3D graphics and video processing capabilities within VMs through partitioned host GPU hardware solutions. The goal is to support hardware with good open-source drivers over hardware with proprietary drivers.

Additionally, long-term resource scheduling mechanisms are being established within the OpenStack framework to cater to the substantial demands of large-scale interactive remote desktop sessions. At the moment, there are significant progressions in aspects 3 and 4, and initial demonstrations are becoming possible. Next steps would include usecases with domain specific VMs in NFDI4BIOIMAGE.

Furthermore, there will be an exchange on common topics and working areas, and considerations for developing a Memorandum of Understanding (MoU) between DataPLANT and NFDI4BIOIMAGE. This includes jointly planned activities for Base4NFDI and the provision of an online training platform based on VDI.