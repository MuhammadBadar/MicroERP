import { Injectable } from '@angular/core';
import { AppConstants } from '../app.constants';
import { UserVM } from '../views/security/models/user-vm';
import { Observable } from 'rxjs';
import { Globals } from '../globals';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  role: string
  constructor(private http: HttpClient,) { }
  async GetRole(userId: string): Promise<string> {
    try {
      var user = new UserVM
      user.id=userId
      const res: any = await this.http.post(Globals.BASE_API_URL + 'User/Search', user).toPromise();
      return res[0].role;
    } catch (error) {
      console.error('Error:', error);
      throw error; 
    }
  }
  SearchUser(data: UserVM): Observable<UserVM[]> {
    return this.http.post<UserVM[]>(Globals.BASE_API_URL + 'User/Search', data).pipe();
  }
}
