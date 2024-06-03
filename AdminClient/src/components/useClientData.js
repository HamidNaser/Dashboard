import axios from "axios";
import { useState } from "react";

export default function useClientData() {
  let data = {};

  const [downloadLoading, setDownloadLoading] = useState(false);
  data.downloadLoading = downloadLoading;

  const [progress, setProgress] = useState(0);
  data.progress = progress;

  const downloadBlobFile = async (blob, fileName) => {
    try {
      if (blob instanceof Blob) {
        const downloadUrl = window.URL.createObjectURL(blob);
        const link = document.createElement("a");
        link.href = downloadUrl;
        link.setAttribute("download", `${fileName}.json`);
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
      } else {
        console.error("The provided data is not a Blob object.");
      }
    } catch (err) {
      console.log("Error:", err);
    }
  };

  const downloadHelp = async (clientId, sessionId) => {
    setDownloadLoading(true);
    try {
      const api = import.meta.env.VITE_BASE_URL + "/AdminApi/OpenHelpUrl";

      const res = await axios.post(api, { clientId: clientId, sessionId: sessionId });

      await downloadBlobFile(res.data, "help.json");
    } catch (err) {
      console.log("error :", err);
    } finally {
      setDownloadLoading(false);
    }
  };

  const downloadSessionResults = async (clientId, sessionId) => {
    setDownloadLoading(true);
    try {
      const api = import.meta.env.VITE_BASE_URL + "/AdminApi/DownloadSessionResults";

      const res = await axios.post(
        api,
        { clientId, sessionId },
        {
          responseType: "blob",
          onDownloadProgress: (progressEvent) => {
            const progress = Math.round((progressEvent.loaded / progressEvent.total) * 100);
            setProgress(progress);
          },
        }
      );
      await downloadBlobFile(res.data, "session_results");
    } catch (err) {
      console.log("error :", err);
    } finally {
      setDownloadLoading(false);
      setTimeout(() => {
        setProgress(0);
      }, 1000);
    }
  };

  const downloadSessionInput = async (clientId, sessionId) => {
    setDownloadLoading(true);
    try {
      const api = import.meta.env.VITE_BASE_URL + "/AdminApi/DownloadInput";

      const res = await axios.post(
        api,
        { clientId, sessionId },
        {
          responseType: "blob",
          onDownloadProgress: (progressEvent) => {
            const progress = Math.round((progressEvent.loaded / progressEvent.total) * 100);
            setProgress(progress);
          },
        }
      );

      await downloadBlobFile(res.data, "session_input");
    } catch (err) {
      console.log("error :", err);
    } finally {
      setDownloadLoading(false);
      setTimeout(() => {
        setProgress(0);
      }, 1000);
    }
  };

  const downloadLiveData = async (clientId) => {
    setDownloadLoading(true);
    try {
      const api = import.meta.env.VITE_BASE_URL + "/AdminApi/DownloadLiveInput";

      const res = await axios.post(
        api,
        { clientId },
        {
          responseType: "blob",
          onDownloadProgress: (progressEvent) => {
            const progress = Math.round((progressEvent.loaded / progressEvent.total) * 100);
            setProgress(progress);
          },
        }
      );

      await downloadBlobFile(res.data, "live_data");
    } catch (err) {
      console.log("error :", err);
    } finally {
      setDownloadLoading(false);
      setTimeout(() => {
        setProgress(0);
      }, 1000);
    }
  };

  const downloadBackUp = async (clientId, sessionId) => {
    setDownloadLoading(true);
    try {
      const api = import.meta.env.VITE_BASE_URL + "/AdminApi/DownloadBackup";

      const res = await axios.post(
        api,
        { clientId, sessionId },
        {
          responseType: "blob",
          onDownloadProgress: (progressEvent) => {
            const progress = Math.round((progressEvent.loaded / progressEvent.total) * 100);
            setProgress(progress);
          },
        }
      );

      await downloadBlobFile(res.data, "back_up");
    } catch (err) {
      console.log("error :", err);
    } finally {
      setDownloadLoading(false);
      setTimeout(() => {
        setProgress(0);
      }, 1000);
    }
  };

  const restoreBackUp = async (clientId, sessionId, locationGroups = null, providerGroups = null) => {
    setDownloadLoading(true);
    try {
      const api = import.meta.env.VITE_BASE_URL + "/AdminApi/RestoreBackupFromFromSession";
      const restoreType = "Full"; // or 'Partial' based on your requirement

      const res = await axios.post(api, { clientId, sessionId, restoreType, locationGroups, providerGroups });

      console.log(res);
    } catch (err) {
      console.log("error :", err);
    } finally {
      setDownloadLoading(false);
    }
  };

  let fns = {
    downloadHelp,
    downloadSessionResults,
    downloadSessionInput,
    downloadLiveData,
    downloadBackUp,
    restoreBackUp,
  };
  return { data, fns };
}
