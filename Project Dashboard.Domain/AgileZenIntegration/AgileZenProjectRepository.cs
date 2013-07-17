namespace ProjectDashboard.Domain
{
    using System;
    using System.Xml;
    using Model.Projects;
    using Helpers;

    public class AgileZenProjectRepository : AgileZenBase, IProjectRepository
    {
        #region Constructor

        public AgileZenProjectRepository(int projectId, string apiKey)
            : base(projectId, apiKey)
        {
        }

        #endregion

        #region Methods

        public Project GetProject()
        {
            var node = GetProjectAsXml();
            var project = new Project
                              {
                                  Id = int.Parse(node.GetText("id")),
                                  CreatedDate = DateTime.Parse(node.GetText("createTime")),
                                  Name = node.GetText("name"),
                                  Description = node.GetText("description"),
                                  Owner = node.GetText("owner/name"),
                              };
            return project;
        }

        #endregion

        #region Helpers

        private XmlNode GetProjectAsXml()
        {
            var url = string.Format("{0}projects/{1}.xml?apikey={2}", ApiRootUrl, ProjectId, ApiKey);
            return GetAgileResponseAsXml(url).DocumentElement;
        }

        #endregion
    }
}
