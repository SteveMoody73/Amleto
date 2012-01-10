using System;
using System.Collections.Generic;
using RemoteExecution.Jobs;

namespace RemoteExecution
{
    [Serializable]
    public class EventBridge : MarshalByRefObject
    {
        private ServerServices.StatusClientChange _clientStatus;
        private ServerServices.StatusProjectChange _projectStatus;
        private ServerServices.StatusFinishedFrameChange _imagePreview;
        private ServerServices.StatusStringChange _messageConsumer;
        private SendLogJob.LogReceiver _logReceiver;

        public EventBridge(ServerServices.StatusClientChange clientStatus,
            ServerServices.StatusProjectChange projectStatus,
            ServerServices.StatusFinishedFrameChange imagePreview,
            ServerServices.StatusStringChange messageConsumer,
            SendLogJob.LogReceiver logReceiver)
        {
            _clientStatus = clientStatus;
            _projectStatus = projectStatus;
            _imagePreview = imagePreview;
            _messageConsumer = messageConsumer;
            _logReceiver = logReceiver;
        }

        public void ClientRefresh(List<ClientConnection> clients)
        {
            _clientStatus.Invoke(clients);
        }

        public void ProjectRefresh(List<RenderProject> projects)
        {
            _projectStatus.Invoke(projects);
        }

        public void ImagePreview(FinishedFrame frame)
        {
            _imagePreview.Invoke(frame);
        }

        public void MessageConsume(string msg)
        {
            _messageConsumer.Invoke(msg);
        }

        public void ReceiveLog(List<string> log)
        {
            _logReceiver.Invoke(log);
        }
    }
}
