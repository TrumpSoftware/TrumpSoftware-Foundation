﻿using System;

namespace TrumpSoftware.RemoteResourcesLibrary
{
    internal static class ResourceFactory
    {
        internal static Resource CreateResource(ResourceFolderLocations resourceFolderLocations, ResourceInfo localResourceInfo, ResourceInfo remoteResourceInfo)
        {
            if (localResourceInfo == null && remoteResourceInfo == null)
                throw new ArgumentException("Local and remote resource infos are null");

            if (localResourceInfo == null)
            {
                localResourceInfo = new ResourceInfo(remoteResourceInfo)
                {
                    Version = 0
                };
            }
            else if (remoteResourceInfo == null)
            {
                remoteResourceInfo = localResourceInfo;
            }

            return new Resource(resourceFolderLocations, localResourceInfo, remoteResourceInfo);
        }
    }
}