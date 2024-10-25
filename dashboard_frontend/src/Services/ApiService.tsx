import axios, { AxiosInstance } from 'axios';

class ApiService {
  private axiosInstance: AxiosInstance;
  
  constructor(baseURL: string) {
    this.axiosInstance = axios.create({
      baseURL,
      headers: {
        'Content-Type': 'application/json',
      },
    });
    
    this.setupInterceptors();
  }

  private setupInterceptors() {
    this.axiosInstance.interceptors.request.use(
      (config) => {
        const token = localStorage.getItem('authToken');
        if (token) {
          config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
      },
      (error) => {
        return Promise.reject(error);
      }
    );

    this.axiosInstance.interceptors.response.use(
      (response) => response,
      (error) => {
        if (error.response.status === 401) {
          console.error('Unauthorized, redirecting to login...');
        }
        return Promise.reject(error);
      }
    );
  }

  public login(email: string, password: string) {
    return this.axiosInstance.post('/users/login', { email, password });
  }

  public register(name: string, email: string, password: string) {
    return this.axiosInstance.post('/users/register', { name, email, password });
  }

  public allTickers() {
    return this.axiosInstance.get('/tickers');
  }

  public getPersonalTickers()
  {
    return this.axiosInstance.get('/personaltickers');
  }

  public updatePersonalTickers(tickers: string[])
  {
    return this.axiosInstance.post('/personaltickers', {Tickers:tickers});

  }
}

export default new ApiService('http://localhost:8080/api'); 