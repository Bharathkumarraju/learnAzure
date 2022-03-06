REducing the cost:
--------------------------->

1. Auto Shutdown
2. Reserved Instances
3. Spot Instances
4. Disk Optimization

Disk:
premium SSD
standard SSD  --> App Servers and In-memory cache


Availability of VMs:
--------------------------->
Fault Domain  --> one rack --> its a physical hardware ... i.e. server rack
Update Domain  --> Host machine reboot  --> its a logical definiton

So naturallu we want to make sure our servers are spread across more than one Fault Domain(more than one rack)  and more than one Update domain

Availability Set:
Is a collection of Fault Domains and update domains


Availability Zone:
------------------------>


AZure CLI:
---------------------------->

bharathkumar@Azure:~$ cd clouddrive
bharathkumar@Azure:~/clouddrive$ cd templates
bharathkumar@Azure:~/clouddrive/templates$ ls -larth
total 13K
drwxrwxrwx 2 bharathkumar bharathkumar    0 Feb  9 08:53 ..
drwxrwxrwx 2 bharathkumar bharathkumar    0 Mar  6 01:08 .
-rwxrwxrwx 1 bharathkumar bharathkumar 3.1K Mar  6 01:09 parameters.json
-rwxrwxrwx 1 bharathkumar bharathkumar 9.2K Mar  6 01:09 template.json
bharathkumar@Azure:~/clouddrive/templates$ 



bharathkumar@Azure:~/clouddrive/templates$ az deployment group create --resource-group optimized-vm-rg --template-file template.json --parameters parameters.json 
 | Running ..

bharathkumar@Azure:~/clouddrive/templates$ 
