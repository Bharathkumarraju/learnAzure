Azure services for IoT(Internet of Things)

Alerts
Insights
Actions


1. Exploring Azure IoT Central:
---------------------------------------------------------------------------->
Azure IOT Hub:  
      Connecting Devices to the Cloud, Managing Devices and Ingesting Data.
      Bidirectional Communication.
      Automatic Provisioning of device objects.
      Its Platform services.

Azure IOT Central: 
     Its a "Managed App Platform"
     Faster to start developing solutions.
     Industry-specific templates.

Azure Sphere:
     Device standards and security are two big issues in IOT ... while azure sphere is intended to solve both of those.
     Its an application platform made of Micro-controller Unit and linux based operating system
     Devices that use azure sphere service 
       1. verify security of operating system on start-up.
       2. microsoft pushes OS updates and security patches.

2. Understanding Big Data Solutions
------------------------------------------------------------------------------>
BigData  is consists of Technologies and strategies that
   1. Gather large datasets.
   2. Organize the data.
   3. Process the data.
   4. Gather insights from the data.

Insights:
Before Bigdata data analysts would gather information into spreadsheets and do all sorts of fancy manipulation and visualizations on that data.
Volume
Velocity
Variety

Ingest --> Persist --> Analyze --> Visualize

Data warehouse(Auzre Synapse)

Batch processing:
   - Hadoop MapReduce
   - Apache Spark

Real-time processing:
   - Spark
   - Apache Storm, Kafka
   - Azure Stream Analytics

R, Python, Scala
C#, .net, java

Querying and Reporting:
-------------------------->
  - Power BI 
  - MS Excel
  - Jupyter Notebooks

i). Azure HDInsight
==========================>
This is microsofts Managed platform for running open-source analytics tools like Apache Hadoop,Spark and Kafka.

We get cluster of computing nodes that gets up and down on-demand as well autoscale.
And also get integration with other Azure services like Data Factory, Data Lake Storage, Cosmos DB, Blob Storage and Event Hub.

Hadoop Distributed File System(DFS) --> is one of the Technologies that really started the big data analytics.
MaPreduce for batch processing
also supports Apache Spark
also supports visual studio

ii). Azure Databricks
==============================>
Popular analysis tool for bigdata that microsoft decided to offer as a hosted platform.
Its based on the Apache Spark analytics platform and was actually designed by founders of spark.
With azure Databricks we get fully managed spark clusters.
And it has a serverless option.
Notebooks
interactive dashboards

iii). Azure Synapse Analytics
====================================>
Formerly called as Azure SQL Data Warehouse.

Data warehouses are large, ordered repositories of data that can be used for analysis and reporting.
A Data lake is composed of more raw data before its been prepared for big data analytics.


3. Exploring Azure Synapse Analytics:
-------------------------------------------------------------------------------->

Azure Synapse Workspace

Azure Synapse Studio --> web.azuresynapse.net
SQL Pools
Apache Spark Pools
Linked Services ---> Power BI, Storage account i.e. blob storage

Notebooks
SQL Scripts


4. Azure Machine Learning:
-------------------------------------------------------------------------------->
Using existing data to forecast future behaviors, outcomes and trends

Machine Learning Studio


5. Azure Cognitive Services:
-------------------------------------------------------------------------------->
These are AI capabilities that are prebuilt that developers can include in their applications using SDKs and APIs within Azure.

The services that are available fall into 5 main pillars
1.Vision
  Text extract from an Image
  Face API
  From Recognizer

2.Speech
   speech to text apis 
   text to speech apis

3.Language
  Language understanding APIs
  Natural Language input
  Sentiment analysis
  Tanslator Service

4.Web Search
  Bing image search APIs

5.Decision
  Anamoly API 

6. Azure Bot Service:
------------------------------------------------------------------------------------->
Its an AI service using Natural Language Processing(NLP)

Bot Framework SDK
Bot Framework Composer 


