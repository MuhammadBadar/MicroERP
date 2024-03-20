import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { VocabularyVM } from './Models/VocabularyVM';
import { Globals } from 'src/app/globals';
import { StorageService } from 'src/app/storage.service';
import { AppConstants } from 'src/app/app.constants';

@Injectable({
  providedIn: 'root'
})
export class VocabularyService {

  constructor(private http: HttpClient, private storeSvc: StorageService) { }
  UpdateVocabulary(value: VocabularyVM): Observable<any> {
    return this.http.put(Globals.BASE_API_URL + 'Vocabulary', value);
  }
  SearchVocabulary(value: VocabularyVM): Observable<VocabularyVM[]> {
    return this.http.post<VocabularyVM[]>(Globals.BASE_API_URL + 'Vocabulary/Search', value).pipe();
  }
  GetVocabulary(): Observable<VocabularyVM[]> {
    return this.http.get<VocabularyVM[]>(Globals.BASE_API_URL + 'Vocabulary').pipe();
  }
  SaveVocabulary(value): Observable<any> {
    return this.http.post(Globals.BASE_API_URL + 'Vocabulary', value);
  }
  DeleteVocabulary(id): Observable<any> {
    debugger
    var vocab = new VocabularyVM
    vocab.id = id
    vocab.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    return this.http.post(Globals.BASE_API_URL + 'Vocabulary/Delete', vocab);
  }

}
