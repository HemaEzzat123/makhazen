import axios from "axios";

const API_URL = import.meta.env.VITE_BACKEND_API_URL;
console.log("Backend API URL:", API_URL);

export const authService = {
  login: async (email, password) => {
    const response = await axios.post(`${API_URL}/api/Accounts/Login`, {
      email,
      password,
    });
    if (response.data.token) {
      localStorage.setItem("token", response.data.token);
      localStorage.setItem("refreshToken", response.data.refreshToken);
    }
    return response.data;
  },

  register: async (userData) => {
    const response = await axios.post(
      `${API_URL}/api/Accounts/Register`,
      userData
    );
    return response.data;
  },

  logout: () => {
    localStorage.removeItem("token");
    localStorage.removeItem("refreshToken");
  },

  forgotPassword: async (email) => {
    const response = await axios.post(
      `${API_URL}/api/Accounts/forget-password`,
      {
        email,
      }
    );
    return response.data;
  },

  resetPassword: async (token, newPassword) => {
    const response = await axios.post(
      `${API_URL}/api/Accounts/reset-password`,
      {
        token,
        newPassword,
      }
    );
    return response.data;
  },

  refreshToken: async () => {
    const refreshToken = localStorage.getItem("refreshToken");
    const response = await axios.post(
      `${API_URL}/api/Accounts/LoginWithRefreshToken`,
      {
        refreshToken,
      }
    );
    if (response.data.token) {
      localStorage.setItem("token", response.data.token);
    }
    return response.data;
  },

  getProfile: async () => {
    const response = await axios.get(`${API_URL}/api/Users/profile`);
    return response.data;
  },

  updateProfile: async (profileData) => {
    const response = await axios.put(
      `${API_URL}/api/Users/update-profile`,
      profileData
    );
    return response.data;
  },
};

// Axios interceptor for handling token refresh
axios.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;
    if (error.response.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;
      try {
        await authService.refreshToken();
        return axios(originalRequest);
      } catch (refreshError) {
        authService.logout();
        window.location.href = "/login";
        return Promise.reject(refreshError);
      }
    }
    return Promise.reject(error);
  }
);
