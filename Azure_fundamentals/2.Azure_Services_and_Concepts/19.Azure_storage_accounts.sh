Azure Blob Storage  --> is for unstructured data like files and documents
(Archive Storage)
can be accessed using https://mystorageaccount.blob.core.windows.net


azure File Storage --> it supports the SMB protocol so it can be attached to virtual machines like network drive
can be accessed using https://mystorageaccount.file.core.windows.net


Azure Disk Storage  --> stores the VM disks used by IaaS VMs


Azure Table Storage --> stores the structural data in the form of NoSQL non-relational data.
can be accessed using https://mystorageaccount.table.core.windows.net


Azure Queue Storage --> queue service for stateless apps.
can be accessed using https://mystorageaccount.queue.core.windows.net


General features of azure Storage:
------------------------------------------>
Data is durable and highly available.

Locally Redundant Storage(LRS)
---------------------------------------------------->
The data is stored three times in the primary datacenter by default.


Authorization to Data:
------------------------------------------------------------------------------------>
1. RBAC in Azure AD 
2. Storage Account Keys
3. Shared Access signature  -- Security token string (SAS Token)

REST APIs
SDK
AZURE CLI
Azure Storage explorer
AzCopy


Azure Files: SMB Port:445 
Azure file shares can be cached on windows servers with "Azure File Sync" for fast access, close to where the data being used.

Azure blob:
Blob: Binary Large OBject
docs,videofiles,text files even VM disks
  1. Block blobs --> stores text and binary data
  2. Append blobs  --> good choice for logs
  3. Page blobs --> stores random access files upto 8TB in size (IaaS VM disks)

Bloc Access tiers
  1. Hot tier  --> Hot access tier is for the data that access frequently, so it has the highest storage cost but the lowest transaction cost
  2. Cool Tier --> is for storing infrequently accessed data so that the storage cost is lower, but the transaction costs are higher.
  3. Archive tier --> 

  Blob Snapshots
  Blob leases
  Soft Delete
  Static Website Hosting
  CDN integration
  Azure Search integration

  


