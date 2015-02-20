using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerceFX.Data;
using NLog;
using RestSharp;

 namespace ECommerceFX.Web.Services
{
    public abstract class RestService<TEntity> : IService<TEntity>
        where TEntity : class, IEntity, new()
    {
        protected const string DataApi = "http://localhost:3227/api/data/";
        private readonly IRestClient _client;
        protected readonly string BaseUrl;
        protected Logger Log = LogManager.GetCurrentClassLogger();
        protected readonly string EntityName = typeof (TEntity).Name;

        protected RestService(IRestClient client, string baseUrl)
        {
            _client = client;
            _client.BaseUrl = new Uri(DataApi);
            BaseUrl = baseUrl;
        }

        public IEnumerable<TEntity> All()
        {
            Log.Info("Retrieving all {0}", EntityName);
            var request = new RestRequest(BaseUrl + "/all");
            return RequestAll(request);
        }
            
        public virtual TEntity ById(Guid id)
        {
            Log.Info("Retrieving {0} with id {1}", EntityName, id);
            var request = new RestRequest(BaseUrl + "/id/{id}")
                .AddUrlSegment("id", id.ToString());
            return Request(request);
        }

        public TEntity Create(TEntity entity)
        {
            Log.Info("Creating entity of type {0}", EntityName);
            var request = new RestRequest(BaseUrl + "/create", Method.POST)
                .AddBody(entity); 
            return Request(request);
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken token)
        {
            Log.Info("Creating entity of type {0} asynchronously", EntityName);
            var request = new RestRequest(BaseUrl + "/create", Method.POST)
                .AddBody(entity);
            return await RequestAsync(request, token);
        }

        public void Delete(Guid id)
        {
            Log.Info("Deleting entity of type {0} with id {1}", EntityName, id);
            var request = new RestRequest(BaseUrl + "/delete/{id}", Method.POST)
                .AddUrlSegment("id", id.ToString());
            Request(request);
        }

        public async Task DeleteAsync(Guid id, CancellationToken token)
        {
            Log.Info("Deleting entity of type {0} with id {1} asynchronously", EntityName, id);
            var request = new RestRequest(BaseUrl + "/delete/{id}", Method.POST)
                .AddUrlSegment("id", id.ToString());
            await RequestAsync(request, token);
        }

        public TEntity Update(TEntity entity)
        {
            Log.Info("Updating entity of type {0}", EntityName);
            var request = new RestRequest(BaseUrl + "update/{id}", Method.POST)
                .AddUrlSegment("id", entity.Id.ToString())
                .AddBody(entity);
            return Request(request);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token)
        {
            Log.Info("Updating entity of type {0} asynchronously", EntityName);
            var request = new RestRequest(BaseUrl + "update/{id}", Method.POST)
                .AddUrlSegment("id", entity.Id.ToString())
                .AddBody(entity);
            return await RequestAsync(request, token);
        }

        protected TEntity Request(IRestRequest request)
        {
            var response = _client.Execute<Data.Messages.RestResponse<TEntity>>(request).Data;
            if (response.IsError)
            {
                Log.Error("Error {0} making request for {1} during a {2} request : {3}",
                    response.StatusCode,
                    EntityName,
                    request.Method,
                    response.Message);
//                throw new RequestExecutionException(new Exception(string.Format("Error occured: {0} - {1}:",
//                    response.StatusCode,
//                    response.Message)));
            }
            return response.Data;
        }

        protected async Task<TEntity> RequestAsync(IRestRequest request, CancellationToken token)
        {
            var response = await _client.ExecuteTaskAsync<Data.Messages.RestResponse<TEntity>>(request, token);
            var responseData = response.Data;
            if (responseData.IsError)
            {
                Log.Error("Error {0} making request for {1} during a {2} request",
                    responseData.StatusCode,
                    EntityName,
                    request.Method);
                throw new Exception(string.Format("Error occured: {0} - {1}:", responseData.StatusCode,
                    responseData.Message));
            }
            return responseData.Data;
        }

        protected IEnumerable<TEntity> RequestAll(IRestRequest request)
        {
            var response = _client.Execute<Data.Messages.RestResponse<List<TEntity>>>(request).Data;
            if (response.IsError)
            {
                Log.Error("Error {0} making request for {1} during a {2} request",
                    response.StatusCode,
                    EntityName,
                    request.Method);
                throw new Exception(string.Format("Error occured: {0} - {1}:", response.StatusCode,
                    response.Message));
            }
            return response.Data;
        }

        protected async Task<IEnumerable<TEntity>> RequestAllAsync(IRestRequest request, CancellationToken token)
        {
            var testResponse = await _client.ExecuteTaskAsync<Data.Messages.RestResponse<List<TEntity>>>(request, token);
            var responseData = testResponse.Data;
            if (responseData.IsError)
            {
                Log.Error("Error {0} making request for {1} during a {2} request",
                    responseData.StatusCode,
                    EntityName,
                    request.Method);
                throw new Exception(string.Format("Error occured: {0} - {1}:", responseData.StatusCode,
                    responseData.Message));
            }
            return responseData.Data;
        }
    }
}